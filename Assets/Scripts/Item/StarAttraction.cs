using UnityEngine;

/// <summary>
/// 「とっておき/StarAttraction」：相手の選択傾向を特定の方向に引き寄せる
/// </summary>
public class StarAttraction : ItemBase
{
    public StarAttraction(RarityEnum rarity)
    {
        Name = "";
        Rarity = rarity;
    }
    
    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}