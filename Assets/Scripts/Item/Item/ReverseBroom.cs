using System;
using UnityEngine;

/// <summary>
/// 「逆行のほうき/ReverseBroom」：一つ前のターンに戻す（最高レアリティのみ）
/// </summary>
public class ReverseBroom : ItemBase
{
    public ReverseBroom(RarityEnum rarity) : base(rarity, ItemTypeEnum.SealPage)
    {
        Name = "";
    }

    public override IItemEffect CreateEffect()
    {
        throw new NotImplementedException();
    }

}