using System;
using System.Collections.Generic;

/// <summary>
/// アイテムを管理するクラス用のインターフェース
/// </summary>
public interface IItemManager
{
    void AddActiveEffect(ActiveEffect activeEffect);
    void UpdateTurn();
    List<DirectionEnum> ShowDirectionSelectionUI(int count);
    event Action<DirectionEnum> UseLimitItem;
    event Action<DirectionEnum> RemoveLimitItem;
}