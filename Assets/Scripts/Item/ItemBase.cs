using UnityEngine;

/// <summary>
/// アイテムの基底クラス
/// </summary>
public abstract class ItemBase
{
    public string Name { get; protected set; }
    public string Description { get; protected set; }
    public RarityEnum Rarity { get; }
    public ItemTypeEnum Type { get; }
    
    protected ItemBase(RarityEnum rarity, ItemTypeEnum type)
    {
        Rarity = rarity;
        Type = type;
    }
    
    /// <summary>
    /// アイテム使用時に特定の効果を生成する
    /// </summary>
    public abstract IItemEffect CreateEffect();
    
    /// <summary>
    /// アイテム使用
    /// </summary>
    public virtual void Use(IBattleMediator mediator)
    {
        var effect = CreateEffect();
        mediator.RegisterEffect(effect);
    }
}