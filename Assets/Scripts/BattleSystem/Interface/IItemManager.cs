using System;

/// <summary>
/// アイテムを管理するクラス用のインターフェース
/// </summary>
public interface IItemManager
{
    void AddActiveEffect(ActiveEffect activeEffect);
    void UpdateTurn();
    event Action<DirectionEnum> UseLimitItem;
    event Action<DirectionEnum> RemoveLimitItem;

}
