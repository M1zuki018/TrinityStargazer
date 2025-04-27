using System;
using System.Collections.Generic;

/// <summary>
/// 「封印のページ/SealPage」：指定した○方向を○ターンの間使用禁止にする（禁止方向に✕マーク）
/// </summary>
public class SealPage : ItemBase
{
    // レアリティごとの効果をまとめた辞書
    private static readonly Dictionary<RarityEnum, (int limit, int effectiveTurns)> RarityEffects = new()
    {
        { RarityEnum.N,   (limit: 1, effectiveTurns: 2) },
        { RarityEnum.C,   (limit: 1, effectiveTurns: 3) },
        { RarityEnum.R,   (limit: 1, effectiveTurns: 4) },
        { RarityEnum.SR,  (limit: 2, effectiveTurns: 3) },
        { RarityEnum.SSR, (limit: 2, effectiveTurns: 4) }
    };

    private int _limitCount;
    private int _effectiveTurns;
    
    public SealPage(RarityEnum rarity) : base(rarity, ItemTypeEnum.SealPage)
    {
        Name = "封印のページ";
        EffectSetting(rarity);
        Description = $"{_limitCount}方向を{_effectiveTurns}ターンの間使用禁止にします";
    }
    
    public override IItemEffect CreateEffect()
    {
        return new SealPageEffect(_limitCount, _effectiveTurns);
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