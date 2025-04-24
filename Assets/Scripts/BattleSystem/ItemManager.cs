using System;
using System.Collections.Generic;

/// <summary>
/// バトルに関わるアイテム効果を管理するためのクラス
/// </summary>
public class ItemManager : IItemManager
{
    private List<ActiveEffect> _activeItems = new List<ActiveEffect>(); // 効果発動中のアイテムのリスト
    private IItemManager _itemManagerImplementation;

    public event Action<DirectionEnum> UseLimitItem; // 方向制限
    public event Action<DirectionEnum> RemoveLimitItem; // 方向制限解除
    
    /// <summary>
    /// アイテム効果を追加する
    /// </summary>
    public void AddActiveEffect(ActiveEffect activeEffect)
    {
        _activeItems.Add(activeEffect);
        activeEffect.EffectData.ApplyEffect(this);
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
            // 持続ターン数のカウントを減らす
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
            effect.EffectData.RemoveEffect(this);
            _activeItems.Remove(effect);
        }
    }
    
    public List<DirectionEnum> ShowDirectionSelectionUI(int count)
    {
        throw new NotImplementedException();
    }
}