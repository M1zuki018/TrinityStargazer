using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 方向決定のロジックを担当するクラス
/// </summary>
public class DirectionSelector : IDirectionSelector
{
    private const float DEFAULT_PROBABILITY = 0.125f;
    
    private Dictionary<DirectionEnum, float> _directionProbabilities = new Dictionary<DirectionEnum, float>();
    private HashSet<DirectionEnum> _limitedDirections = new HashSet<DirectionEnum>();
    private Dictionary<DirectionEnum, float> _originalProbabilities = new Dictionary<DirectionEnum, float>();
    
    public event Action<DirectionEnum> OnLimitedDirection; // 方向制限がかかったときに呼び出されるイベント
    public event Action<DirectionEnum> OnUnlimitedDirection; // 方向制限が解除されたときに呼び出されるイベント
    public event Action<DirectionEnum> OnEnemyDirectionChanged; // 敵が向く方向が決まった時に呼び出されるイベント

    public DirectionSelector()
    {
        InitializeProbabilities();
    }
    
    private void InitializeProbabilities()
    {
        // 辞書を作成（Enumと初期倍率のkvペア）
        foreach (DirectionEnum direction in Enum.GetValues(typeof(DirectionEnum)))
        {
            _directionProbabilities[direction] = DEFAULT_PROBABILITY;
        }
    }

    /// <summary>
    /// 制限中の方向を返す
    /// </summary>
    public HashSet<DirectionEnum> GetSealedDirections() => _limitedDirections;

    /// <summary>
    /// 方向の確率を変更する
    /// </summary>
    public void ModifyProbability(DirectionEnum direction, float addedProbability)
    {
        // 制限中の方向は変更しない
        if (_limitedDirections.Contains(direction))
            return;
            
        float originalProbability = _directionProbabilities[direction];
        float newProbability = originalProbability + addedProbability;
        newProbability = Mathf.Clamp01(newProbability); // 確率の範囲は0~1に制限する
        
        float probabilityDifference = newProbability - originalProbability;
    
        // 他の方向から確率を引く（選ばれた方向以外から比例配分で減らす）
        if (probabilityDifference > 0)
        {
            // 調整可能な方向の合計確率を計算
            float adjustableProbabilitySum = 0;
            foreach (var dir in _directionProbabilities.Keys)
            {
                if (dir != direction && !_limitedDirections.Contains(dir))
                {
                    adjustableProbabilitySum += _directionProbabilities[dir];
                }
            }
        
            // 調整可能な方向がない場合は終了
            if (adjustableProbabilitySum <= 0)
            {
                Debug.LogWarning("他の方向の確率を調整できません");
                return;
            }
        
            // 各方向の確率を比例配分で減らす
            foreach (var dir in _directionProbabilities.Keys.ToList())
            {
                if (dir != direction && !_limitedDirections.Contains(dir))
                {
                    float reductionRatio = _directionProbabilities[dir] / adjustableProbabilitySum;
                    _directionProbabilities[dir] -= probabilityDifference * reductionRatio;
                
                    // 負の確率にならないように
                    _directionProbabilities[dir] = Mathf.Max(0, _directionProbabilities[dir]);
                }
            }
        }
    
        // 目標方向の確率を設定
        _directionProbabilities[direction] = newProbability;

        foreach (var dir in _directionProbabilities.Keys.ToList())
        {
            Debug.Log($"{dir}{_directionProbabilities[dir]}");
        }
    }

    /// <summary>
    /// 方向に制限をつける
    /// </summary>
    public void LimitProbability(DirectionEnum direction)
    {
        OnLimitedDirection?.Invoke(direction);
        
        // 元の確率を保存
        _originalProbabilities[direction] = _directionProbabilities[direction];
        
        float total = _directionProbabilities.Values.Sum();
        float directionProb = _directionProbabilities[direction];
        
        // 方向を制限済みとしてマーク
        _limitedDirections.Add(direction);
        
        // 確率を0に設定
        _directionProbabilities[direction] = 0;
        
        float newTotal = total - directionProb;
        
        // 残りの方向の確率を再配分
        if (newTotal > 0)
        {
            NormalizeProbabilities(total);
        }
        else
        {
            // すべての方向が制限された場合のフォールバック
            ResetNonLimitedProbabilities();
        }
    }
    
    /// <summary>
    /// 正規化
    /// </summary>
    private void NormalizeProbabilities(float total)
    {
        float normalizeFactor = total / _directionProbabilities.Values.Sum(); // 元の合計と新しい合計
        foreach (var dir in _directionProbabilities.Keys.ToList())
        {
            if (!_limitedDirections.Contains(dir))
            {
                _directionProbabilities[dir] *= normalizeFactor;
            }
        }
    }
    
    /// <summary>
    /// 方向の制限を解除する
    /// </summary>
    public void RemoveLimitProbability(DirectionEnum direction)
    {
        // 制限されていない場合は何もしない
        if (!_limitedDirections.Contains(direction))
            return;
        
        OnUnlimitedDirection?.Invoke(direction);
        _limitedDirections.Remove(direction); // 制限済みリストから削除
        
        float total = _directionProbabilities.Values.Sum();
        
        // 元の確率に戻す（または均等な確率を設定）
        float originalProb = _originalProbabilities.ContainsKey(direction) 
            ? _originalProbabilities[direction] 
            : DEFAULT_PROBABILITY;
            
        _directionProbabilities[direction] = originalProb;
        _originalProbabilities.Remove(direction);
        
        NormalizeProbabilities(total);
    }
    
    /// <summary>
    /// 制限されていない方向の確率を均等にリセット
    /// </summary>
    private void ResetNonLimitedProbabilities()
    {
        int nonLimitedCount = Enum.GetValues(typeof(DirectionEnum)).Length - _limitedDirections.Count;
        
        if (nonLimitedCount <= 0)
            return;
            
        float equalProb = 1.0f / nonLimitedCount;
        
        foreach (DirectionEnum direction in Enum.GetValues(typeof(DirectionEnum)))
        {
            if (!_limitedDirections.Contains(direction))
            {
                _directionProbabilities[direction] = equalProb;
            }
        }
    }
    
    /// <summary>
    /// 確率に基づいて方向を決定する
    /// </summary>
    public DirectionEnum DecideDirection()
    {
        // 全方向が制限されている場合はデフォルト値を返す
        if (_limitedDirections.Count == Enum.GetValues(typeof(DirectionEnum)).Length)
        {
            OnEnemyDirectionChanged?.Invoke(DirectionEnum.Up);
            return DirectionEnum.Up;
        }
        
        float rand = Random.value;
        float cumulativeProbability = 0f;
        
        foreach (var kvp in _directionProbabilities)
        {
            if (_limitedDirections.Contains(kvp.Key))
                continue;
                
            cumulativeProbability += kvp.Value;
            if (rand < cumulativeProbability)
            {
                OnEnemyDirectionChanged?.Invoke(kvp.Key);
                return kvp.Key;
            }
        }
        
        // 浮動小数点の誤差で到達した場合に備えてデフォルト値を返す
        OnEnemyDirectionChanged?.Invoke(DirectionEnum.Up);
        return DirectionEnum.Up;
    }
    
    /// <summary>
    /// 確率をリセットする
    /// </summary>
    public void ResetProbabilities()
    {
        _originalProbabilities.Clear();
        
        foreach (DirectionEnum direction in Enum.GetValues(typeof(DirectionEnum)))
        {
            _directionProbabilities[direction] = DEFAULT_PROBABILITY;
        }
    }
}