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
        Rarity = rarity;
    }
    
    public override void Use()
    {
        throw new System.NotImplementedException();
    }
    
    /// <summary>
    /// 効果を設定する
    /// </summary>
    private void EffectSetting(RarityEnum rarity)
    {
    }
}