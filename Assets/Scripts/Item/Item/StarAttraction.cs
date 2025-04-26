using UnityEngine;

/// <summary>
/// 「とっておき/StarAttraction」：相手の選択傾向を特定の方向に引き寄せる
/// </summary>
public class StarAttraction : ItemBase
{
    public StarAttraction(RarityEnum rarity) : base(rarity, ItemTypeEnum.SealPage)
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