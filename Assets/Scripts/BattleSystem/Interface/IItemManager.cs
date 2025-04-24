using System;

/// <summary>
/// アイテムを管理するクラス用のインターフェース
/// </summary>
public interface IItemManager
{
    public void AddActiveEffect(ActiveEffect activeEffect);
    public void UpdateTurn();
    public event Action<DirectionEnum> UseLimitItem;

}
