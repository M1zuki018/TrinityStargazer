using System;
using System.Collections.Generic;

/// <summary>
/// 「スマートフォン/SmartPhone」：カリルがバトルを手伝ってくれる
/// </summary>
public class SmartPhone : ItemBase
{
    // レアリティごとの効果をまとめた辞書
    private static readonly Dictionary<RarityEnum, int> RarityEffects = new()
    {
        { RarityEnum.SR,  80},
        { RarityEnum.SSR, 100}
    };

    private int _accuracyRate; // 正確性
    
    public SmartPhone(RarityEnum rarity) : base(rarity, ItemTypeEnum.SealPage)
    {
        Name = "スマートフォン";
        EffectSetting(rarity);
        Description = $"カリルを呼びつける";
    }

    public override IItemEffect CreateEffect()
    {
        return new SmartPhoneEffect(_accuracyRate);
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
        
        _accuracyRate = effects;
    }
}
