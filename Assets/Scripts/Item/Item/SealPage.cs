using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 「封印のページ/SealPage」：指定した○方向を○ターンの間使用禁止にする（禁止方向に✕マーク）
/// </summary>
public class SealPage : ItemBase
{
    // レアリティごとの効果をまとめた辞書
    private static readonly Dictionary<RarityEnum, (int limit, int effectiveTurns)> RarityEffects = new()
    {
        { RarityEnum.N,   (limit: 1, effectiveTurns: 2) },
        { RarityEnum.C,   (limit: 1, effectiveTurns: 3) },
        { RarityEnum.R,   (limit: 1, effectiveTurns: 4) },
        { RarityEnum.SR,  (limit: 2, effectiveTurns: 3) },
        { RarityEnum.SSR, (limit: 2, effectiveTurns: 4) }
    };
    
    public int LimitCount { get; private set; }
    public int EffectiveTurns { get; private set; }
    
    public SealPage(RarityEnum rarity) : base(rarity, ItemTypeEnum.SealPage)
    {
        Name = "封印のページ";
        EffectSetting(rarity);
        Description = $"{LimitCount}方向を{EffectiveTurns}ターンの間使用禁止にします";
    }
    
    public override void Use(IItemManager itemManager)
    {
        base.Use(itemManager);
        
        // 方向選択UIを表示
        List<DirectionEnum> selectedDirections = itemManager.ShowDirectionSelectionUI(LimitCount);
        
        if (selectedDirections != null && selectedDirections.Count > 0)
        {
            var effectData = new SealPageEffectData(EffectiveTurns, selectedDirections); // 効果データを作成
            itemManager.AddActiveEffect(new ActiveEffect(Type, effectData)); // アクティブ効果を登録
        }
    }

    /// <summary>
    /// 効果を設定する
    /// </summary>
    private void EffectSetting(RarityEnum rarity)
    {
        if (!RarityEffects.TryGetValue(rarity, out var effects))
        {
            throw new ArgumentException($"未知のレアリティです: {rarity}");
        }
        
        (LimitCount, EffectiveTurns) = effects;
    }
}