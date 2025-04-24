using UnityEngine;

/// <summary>
/// アイテムの基底クラス
/// </summary>
public abstract class ItemBase
{
    public string Name { get; protected set; }
    public string Description { get; protected set; }
    public RarityEnum Rarity { get; protected set; }
    public ItemTypeEnum Type { get; protected set; }
    
    protected ItemBase(RarityEnum rarity, ItemTypeEnum type)
    {
        Rarity = rarity;
        Type = type;
    }
    
    public virtual void Use()
    {
        Debug.Log($"{Name}を使用しました");
    }
}