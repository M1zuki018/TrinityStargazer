using UnityEngine;

/// <summary>
/// アイテム効果データの基底クラス
/// </summary>
public abstract class EffectBase
{
    public int RemainingTurns { get; set; }
    
    public EffectBase(int turns)
    {
        RemainingTurns = turns;
    }
    
    // 効果適用時に呼ばれる
    public abstract void ApplyEffect(IItemManager itemManager);
    
    // 効果終了時に呼ばれる
    public abstract void RemoveEffect(IItemManager itemManager);
}