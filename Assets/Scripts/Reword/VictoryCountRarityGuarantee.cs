using System;
using UnityEngine;

/// <summary>
/// 勝利数に基づくレアリティ保証設定
/// </summary>
[Serializable]
public class VictoryCountRarityGuarantee
{
    [SerializeField] private int _requiredVictoryCount;
    [SerializeField] private RarityEnum _guaranteedRarity;
    
    /// <summary>
    /// 必要勝利数を取得
    /// </summary>
    public int GetRequiredVictoryCount() => _requiredVictoryCount;
    
    /// <summary>
    /// 保証されるレアリティを取得
    /// </summary>
    public RarityEnum GetGuaranteedRarity() => _guaranteedRarity;
}