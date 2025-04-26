using UnityEngine;

/// <summary>
/// 「星の予測盤/CelestialForecast」：次どちらを向くか予測する（確率。レアリティで変更）
/// </summary>
public class CelestialForecast : ItemBase
{
    public CelestialForecast(RarityEnum rarity) : base(rarity, ItemTypeEnum.SealPage)
    {
        Name = "";
    }

    public override IItemEffect CreateEffect()
    {
        throw new System.NotImplementedException();
    }

    public override void Use(IBattleMediator battleMediator)
    {
        throw new System.NotImplementedException();
    }
}