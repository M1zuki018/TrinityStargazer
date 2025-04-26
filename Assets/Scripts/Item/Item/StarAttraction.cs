using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 「とっておき/StarAttraction」：相手の選択傾向を特定の方向に引き寄せる
/// </summary>
public class StarAttraction : ItemBase
{
    // レアリティごとの効果をまとめた辞書
    private static readonly Dictionary<RarityEnum, (int value, int effectiveTurns)> RarityEffects = new()
    {
        { RarityEnum.N,   (value: 15, effectiveTurns: 1) },
        { RarityEnum.C,   (value: 25, effectiveTurns: 1) },
        { RarityEnum.R,   (value: 20, effectiveTurns: 2) },
        { RarityEnum.SR,  (value: 30, effectiveTurns: 2) },
        { RarityEnum.SSR, (value: 25, effectiveTurns: 3) }
    };
    
    private int _value;
    private int _effectiveTurns;
    
    public StarAttraction(RarityEnum rarity) : base(rarity, ItemTypeEnum.SealPage)
    {
        Name = "とっておき";
        EffectSetting(rarity);
        Description = $"とっておきの物で相手の注意を誘導する。{_value}%の確率上乗せする";
    }

    public override IItemEffect CreateEffect()
    {
        return new StarAttractionEffect(_value, _effectiveTurns);
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
        
        (_value, _effectiveTurns) = effects;
    }
}