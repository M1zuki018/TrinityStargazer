using UnityEngine;

/// <summary>
/// 「共鳴ライト/ResonanceLight」：二つの方向をリンクさせ、どちらを選んでも同じ方向として判定（リンクされた方向同士が同じ色の線で繋がる）
/// </summary>
public class ResonanceLight : ItemBase
{
    public ResonanceLight(RarityEnum rarity) : base(rarity, ItemTypeEnum.SealPage)
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