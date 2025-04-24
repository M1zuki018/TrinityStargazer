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
    
    private Dictionary<DirectionEnum, float> _directionProbabilities;
    
    public DirectionDecider()
    {
        InitializeProbabilities();
    }
    
    private void InitializeProbabilities()
    {
        _directionProbabilities = new Dictionary<DirectionEnum, float>();
        
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
        float total = _directionProbabilities.Values.Sum();
        
        _directionProbabilities[direction] += addedProbability;
        
        float newTotal = _directionProbabilities.Values.Sum();
        
        // 正規化
        float normalizeFactor = total / newTotal;
        foreach (var dir in _directionProbabilities.Keys.ToList())
        {
            _directionProbabilities[dir] *= normalizeFactor;
        }
    }
    
    /// <summary>
    /// 確率に基づいて方向を決定する
    /// </summary>
    public DirectionEnum DecideDirection()
    {
        float rand = Random.value;
        float cumulativeProbability = 0f;
        
        foreach (var kvp in _directionProbabilities)
        {
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
        foreach (var dir in _directionProbabilities.Keys.ToList())
        {
            _directionProbabilities[dir] = DEFAULT_PROBABILITY;
        }
    }
}