using System;
using System.Collections.Generic;
/// <summary>
/// バトルに関わるアイテム効果を管理するためのクラス
/// </summary>
public class ItemManager : IItemManager
{
    private List<ActiveEffect> _activeItems = new List<ActiveEffect>(); // 効果発動中のアイテムのリスト
    
    public event Action<DirectionEnum, float> UseModifyDirectionProbabilityItem; // 方向制限

    /// <summary>
    /// 追加する
    /// </summary>
    public void AddActiveEffect(ActiveEffect activeEffect)
    {
        _activeItems.Add(activeEffect);
    }
}
