using UnityEngine;

/// <summary>
/// 各アイテムのベースとなるクラス
/// </summary>
public abstract class ItemBase
{
    public string Name { get; protected set; }
    public RarityEnum Rarity { get; protected set; }
    public Sprite Icon { get; protected set; }
    public abstract void Use();
}
