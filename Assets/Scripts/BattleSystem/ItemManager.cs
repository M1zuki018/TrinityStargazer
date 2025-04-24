using System;
using System.Collections.Generic;
/// <summary>
/// バトルに関わるアイテム効果を管理するためのクラス
/// </summary>
public class ItemManager : IItemManager
{
    private List<ActiveEffect> _activeItems = new List<ActiveEffect>(); // 効果発動中のアイテムのリスト
    
    public event Action<DirectionEnum> UseLimitItem; // 方向制限
    public event Action<DirectionEnum> RemoveLimitItem; // 方向制限解除

    /// <summary>
    /// アイテム効果を追加する
    /// </summary>
    public void AddActiveEffect(ActiveEffect activeEffect)
    {
        _activeItems.Add(activeEffect);
        UseItem();
    }

    private void UseItem()
    {
        
    }

    /// <summary>
    /// ターン更新時の処理
    /// </summary>
    public void UpdateTurn()
    {
        // 削除対象を一時リストに入れる（ループ中の削除を避けるため）
        List<ActiveEffect> effectsToRemove = new List<ActiveEffect>();
        
        foreach (var activeEffect in _activeItems)
        {
            // 持続ターン数のカウントを減らして、ゼロになったものはリストから削除する
            activeEffect.RemainingTurns--;
            if (activeEffect.RemainingTurns <= 0)
            {
                // 効果が切れたものを削除リストに追加
                effectsToRemove.Add(activeEffect);
            }
        }
        
        // 効果が切れたアイテムの後処理
        foreach (var effect in effectsToRemove)
        {
            HandleEffectEnd(effect);
            _activeItems.Remove(effect);
        }
    }
    
    /// <summary>
    /// アイテム効果が終了した時の処理
    /// </summary>
    private void HandleEffectEnd(ActiveEffect effect)
    {
        switch (effect.Type)
        {
            case ItemTypeEnum.SealPage:
                RemoveLimitItem?.Invoke(DirectionEnum.Down); // TODO
                break;
                
            // 他のアイテムタイプの終了処理...
                
            default:
                break;
        }
    }
}
