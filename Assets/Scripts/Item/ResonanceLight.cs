using UnityEngine;

/// <summary>
/// 「共鳴ライト/ResonanceLight」：二つの方向をリンクさせ、どちらを選んでも同じ方向として判定（リンクされた方向同士が同じ色の線で繋がる）
/// </summary>
public class ResonanceLight : ItemBase
{
    public ResonanceLight(RarityEnum rarity)
    {
        Name = "";
        Rarity = rarity;
    }
    
    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}