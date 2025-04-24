using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

/// <summary>
/// 方向決定のロジックを担当するクラス
/// </summary>
public class DirectionDecider : IDirectionDecider
{
    private const float DEFAULT_PROBABILITY = 0.125f;
    
    private Dictionary<DirectionEnum, float> _directionProbabilities = new Dictionary<DirectionEnum, float>();
    private HashSet<DirectionEnum> _limitedDirections = new HashSet<DirectionEnum>();
    private Dictionary<DirectionEnum, float> _originalProbabilities = new Dictionary<DirectionEnum, float>();
    
    public DirectionDecider()
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
    /// 方向の確率を変更する
    /// </summary>
    public void ModifyProbability(DirectionEnum direction, float addedProbability)
    {
        // 制限中の方向は変更しない
        if (_limitedDirections.Contains(direction))
            return;
            
        float total = _directionProbabilities.Values.Sum();
        
        _directionProbabilities[direction] += addedProbability;
        
        // 負の確率にならないよう制限
        if (_directionProbabilities[direction] < 0)
        {
            _directionProbabilities[direction] = 0;
        }
        
        float newTotal = _directionProbabilities.Values.Sum();
        
        // 正規化
        float normalizeFactor = total / newTotal;
        foreach (var dir in _directionProbabilities.Keys.ToList())
        {
            if (!_limitedDirections.Contains(dir))
            {
                _directionProbabilities[dir] *= normalizeFactor;
            }
        }
    }

    /// <summary>
    /// 方向に制限をつける
    /// </summary>
    public void LimitProbability(DirectionEnum direction)
    {
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
            float redistributeFactor = total / newTotal;
            foreach (var dir in _directionProbabilities.Keys.ToList())
            {
                if (!_limitedDirections.Contains(dir))
                {
                    _directionProbabilities[dir] *= redistributeFactor;
                }
            }
        }
        else
        {
            // すべての方向が制限された場合のフォールバック
            ResetNonLimitedProbabilities();
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
            
        float total = _directionProbabilities.Values.Sum();
        
        // 制限済みリストから削除
        _limitedDirections.Remove(direction);
        
        // 元の確率に戻す（または均等な確率を設定）
        float originalProb = _originalProbabilities.ContainsKey(direction) 
            ? _originalProbabilities[direction] 
            : DEFAULT_PROBABILITY;
            
        _directionProbabilities[direction] = originalProb;
        _originalProbabilities.Remove(direction);
        
        float newTotal = _directionProbabilities.Values.Sum();
        
        // 正規化
        float normalizeFactor = total / newTotal;
        foreach (var dir in _directionProbabilities.Keys.ToList())
        {
            _directionProbabilities[dir] *= normalizeFactor;
        }
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
            return DirectionEnum.Up;
            
        float rand = Random.value;
        float cumulativeProbability = 0f;
        
        foreach (var kvp in _directionProbabilities)
        {
            if (_limitedDirections.Contains(kvp.Key))
                continue;
                
            cumulativeProbability += kvp.Value;
            if (rand < cumulativeProbability)
            {
                return kvp.Key;
            }
        }
        
        // 浮動小数点の誤差で到達した場合に備えてデフォルト値を返す
        return DirectionEnum.Up;
    }
    
    /// <summary>
    /// 確率をリセットする
    /// </summary>
    public void ResetProbabilities()
    {
        _limitedDirections.Clear();
        _originalProbabilities.Clear();
        
        foreach (DirectionEnum direction in Enum.GetValues(typeof(DirectionEnum)))
        {
            _directionProbabilities[direction] = DEFAULT_PROBABILITY;
        }
    }
}