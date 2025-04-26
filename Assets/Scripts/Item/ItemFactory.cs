using System;

/// <summary>
/// アイテムファクトリー
/// </summary>
public class ItemFactory
{
    /// <summary>
    /// アイテムのインスタンスを作成する
    /// </summary>
    public ItemBase CreateItem(ItemTypeEnum type, RarityEnum rarity)
    {
        switch (type)
        {
            case ItemTypeEnum.SealPage:
                return new SealPage(rarity);
            
            case ItemTypeEnum.ReverseBroom:
                if(rarity != RarityEnum.SSR)
                    throw new ArgumentException($"[逆行のほうき] {rarity} は設定されていません");
                return new ReverseBroom(rarity);
            
            case ItemTypeEnum.CelestialForecast:
                return new CelestialForecast(rarity);
            
            case ItemTypeEnum.SmartPhone:
                if (rarity == RarityEnum.C || rarity == RarityEnum.N || rarity == RarityEnum.R)
                    throw new ArgumentException($"[スマートフォン] {rarity} は設定されていません");
                return new SmartPhone(rarity);
            
            case ItemTypeEnum.ResonanceCable:
                return new ResonanceCable(rarity);
            
            case ItemTypeEnum.ChallengeRose:
                if(rarity == RarityEnum.N || rarity == RarityEnum.C)
                    throw new ArgumentException($"[決闘の薔薇] {rarity} は設定されていません");
                return new ChallengeRose(rarity);
            
            case ItemTypeEnum.StarAttraction:
                return new StarAttraction(rarity);
            
            default:
                throw new ArgumentException("未知のアイテムタイプです");
        }
    }
}
