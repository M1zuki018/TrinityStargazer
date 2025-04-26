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
    void UsedLimitItem(DirectionEnum direction);
    void RemovedLimitItem(DirectionEnum direction);
    event Action<DirectionEnum> UseLimitItem;
    event Action<DirectionEnum> RemoveLimitItem;
}