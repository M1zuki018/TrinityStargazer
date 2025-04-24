using UnityEngine;

/// <summary>
/// 「スマートフォン/SmartPhone」：カリルが一度だけアドバイスをくれる（相手の傾向を教えてくれる）（テキストボックス・アイコンが出現）
/// </summary>
public class SmartPhone : ItemBase
{
    public SmartPhone(RarityEnum rarity)
    {
        Name = "";
        Rarity = rarity;
    }
    
    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}
