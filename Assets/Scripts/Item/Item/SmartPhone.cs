using UnityEngine;

/// <summary>
/// 「スマートフォン/SmartPhone」：カリルが一度だけアドバイスをくれる（相手の傾向を教えてくれる）（テキストボックス・アイコンが出現）
/// </summary>
public class SmartPhone : ItemBase
{
    public SmartPhone(RarityEnum rarity) : base(rarity, ItemTypeEnum.SealPage)
    {
        Name = "スマートフォン";
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
