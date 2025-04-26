using System;

/// <summary>
/// 「逆行のほうき/ReverseBroom」：一つ前のターンに戻す（最高レアリティのみ）
/// </summary>
public class ReverseBroom : ItemBase
{
    public ReverseBroom(RarityEnum rarity) : base(rarity, ItemTypeEnum.SealPage)
    {
        Name = "逆行のほうき";
        EffectSetting(rarity);
        Description = "一つ前のターンに戻す";
    }

    public override IItemEffect CreateEffect()
    {
        return new ReverseBroomEffect();
    }
    
    /// <summary>
    /// 効果を設定する
    /// </summary>
    private void EffectSetting(RarityEnum rarity)
    {
        if (rarity != RarityEnum.SSR)
        {
            throw new ArgumentException($"未知のレアリティです: {rarity}");
        }
    }
}