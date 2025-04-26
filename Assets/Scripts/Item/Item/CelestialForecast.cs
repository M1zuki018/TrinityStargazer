using System;
using System.Collections.Generic;

/// <summary>
/// 「星の予測盤/CelestialForecast」：次どちらを向くか予測する（確率。レアリティで変更）
/// </summary>
public class CelestialForecast : ItemBase
{
    // レアリティごとの効果をまとめた辞書
    private static readonly Dictionary<RarityEnum, int> RarityEffects = new()
    {
        { RarityEnum.N,   40},
        { RarityEnum.C,   55},
        { RarityEnum.R,   70},
        { RarityEnum.SR,  85},
        { RarityEnum.SSR, 100}
    };

    private int _value;
    
    public CelestialForecast(RarityEnum rarity) : base(rarity, ItemTypeEnum.SealPage)
    {
        Name = "星の予測盤";
        EffectSetting(rarity);
        Description = $"相手が次に向く方向を{_value}%の確率で予測する";
    }

    public override IItemEffect CreateEffect()
    {
        throw new System.NotImplementedException();
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
        
        _value = effects;
    }
}