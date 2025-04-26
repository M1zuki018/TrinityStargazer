using System.Collections.Generic;

/// <summary>
/// 封印のページの効果データ
/// </summary>
public class SealPageEffectData : EffectBase
{
    public List<DirectionEnum> SealedDirections { get; private set; }
    
    public SealPageEffectData(int turns, List<DirectionEnum> sealedDirections) : base(turns)
    {
        SealedDirections = sealedDirections;
        ApplyEffect(BattleSystemPresenter.Instance.ItemManager);
    }
    
    public override void ApplyEffect(IItemManager itemManager)
    {
        foreach (var direction in SealedDirections)
        {
            itemManager.UsedLimitItem(direction);
        }
    }
    
    public override void RemoveEffect(IItemManager itemManager)
    {
        foreach (var direction in SealedDirections)
        {
            itemManager.RemovedLimitItem(direction);
        }
    }
}