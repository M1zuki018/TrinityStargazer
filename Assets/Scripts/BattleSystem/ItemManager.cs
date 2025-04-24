using System;
using System.Collections.Generic;
/// <summary>
/// バトルに関わるアイテム効果を管理するためのクラス
/// </summary>
public class ItemManager : IItemManager
{
    private List<ActiveEffect> _activeItems = new List<ActiveEffect>(); // 効果発動中のアイテムのリスト
    
    public event Action<DirectionEnum> UseLimitItem; // 方向制限

    /// <summary>
    /// 追加する
    /// </summary>
    public void AddActiveEffect(ActiveEffect activeEffect)
    {
        _activeItems.Add(activeEffect);
    }

    /// <summary>
    /// ターン更新時の処理
    /// </summary>
    public void UpdateTurn()
    {
        foreach (var activeEffect in _activeItems)
        {
            // 持続ターン数のカウントを減らして、ゼロになったものはリストから削除する
            activeEffect.RemainingTurns--;
            if (activeEffect.RemainingTurns <= 0)
            {
                //TODO: 効果発動前の状態に戻す処理
                _activeItems.Remove(activeEffect);
            }
        }
    }
}
