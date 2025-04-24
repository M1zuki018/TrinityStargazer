using System;
using System.Collections.Generic;

/// <summary>
/// 「封印のページ/SealPage」：指定した○方向を○ターンの間使用禁止にする（禁止方向に✕マーク）
/// </summary>
public class SealPage : ItemBase
{
    // クラス全体で共有する静的Dictionaryを使用
    private static readonly Dictionary<RarityEnum, (int limit, int effectiveTurns)> RarityEffects = new()
    {
        { RarityEnum.N,   (limit: 1, effectiveTurns: 1) },
        { RarityEnum.C,   (limit: 2, effectiveTurns: 1) },
        { RarityEnum.R,   (limit: 2, effectiveTurns: 2) },
        { RarityEnum.SR,  (limit: 3, effectiveTurns: 2) },
        { RarityEnum.SSR, (limit: 4, effectiveTurns: 3) }
    };

    public int LimitCount { get; private set; }
    public int EffectiveTurns { get; private set; }
    
    public SealPage(RarityEnum rarity) : base(rarity, ItemTypeEnum.SealPage)
    {
        Name = "封印のページ";
        EffectSetting(rarity);
        Description = $"{LimitCount}方向を{EffectiveTurns}ターンの間使用禁止にします";
    }
    
    public override void Use()
    {
        base.Use();
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
        
        (LimitCount, EffectiveTurns) = effects;
    }
}