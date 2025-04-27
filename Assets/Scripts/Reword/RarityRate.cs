using System;
using UnityEngine;

/// <summary>
/// 報酬システムのレアリティごとのレート
/// </summary>
[Serializable]
public class RarityRate
{
    [SerializeField] private RarityEnum _rarity;
    [SerializeField] private float _rate;
    
    /// <summary>
    /// レートを取得
    /// </summary>
    public float GetRate() => _rate;
}
