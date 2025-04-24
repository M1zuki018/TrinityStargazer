using UnityEngine;

/// <summary>
/// 「封印のページ/SealPage」：指定した○方向を○ターンの間使用禁止にする（禁止方向に✕マーク）
/// </summary>
public class SealPage : ItemBase
{
    public SealPage(RarityEnum rarity)
    {
        Name = "";
        Rarity = rarity;
    }
    
    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}