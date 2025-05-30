using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 「共鳴ケーブル/ResonanceCable」：二つの方向をリンクさせ、どちらを選んでも同じ方向として判定（リンクされた方向同士が同じ色の線で繋がる）
/// </summary>
public class ResonanceCable : ItemBase
{
    // レアリティごとの効果をまとめた辞書
    private static readonly Dictionary<RarityEnum, (int limit, int effectiveTurns)> RarityEffects = new()
    {
        { RarityEnum.N,   (limit: 2, effectiveTurns: 1) },
        { RarityEnum.C,   (limit: 3, effectiveTurns: 1) },
        { RarityEnum.R,   (limit: 4, effectiveTurns: 1) },
        { RarityEnum.SR,  (limit: 5, effectiveTurns: 1) },
        { RarityEnum.SSR, (limit: 6, effectiveTurns: 1) }
    };
    
    private int _limitCount;
    private int _effectiveTurns;
    
    public ResonanceCable(RarityEnum rarity) : base(rarity, ItemTypeEnum.ResonanceCable)
    {
        Name = "共鳴ケーブル";
        EffectSetting(rarity);
        Description = $"{_limitCount}つの方向をリンクさせ共通の方向として扱う";

    }

    public override IItemEffect CreateEffect()
    {
        return new ResonanceCableEffect(_limitCount, _effectiveTurns);
    }
    
    /// <summary>
    /// 効果を設定する
    /// </summary>
    private void EffectSetting(RarityEnum rarity)
    {
        if (!RarityEffects.TryGetValue(rarity, out var effects))
        {
            throw new ArgumentException($"未知のレアリティです: {rarity}");
        }
        
        (_limitCount, _effectiveTurns) = effects;
    }
}