using UnityEngine;

/// <summary>
/// 「星の予測盤/CelestialForecast」：次どちらを向くか予測する（確率。レアリティで変更）
/// </summary>
public class CelestialForecast : ItemBase
{
    public CelestialForecast(RarityEnum rarity)
    {
        Name = "";
        Rarity = rarity;
    }
    
    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}