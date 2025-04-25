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
        InventoryManager.Instance.AddItem(type, rarity);
    }
    
    public virtual void Use(IItemManager itemManager)
    {
        Debug.Log($"{Name}を使用しました");
    }
}