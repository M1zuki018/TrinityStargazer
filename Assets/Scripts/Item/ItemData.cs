/// <summary>
/// 各アイテムのベースとなるクラス
/// </summary>
public abstract class ItemData
{
    public string Name { get; protected set; }
    public RarityEnum Rarity { get; protected set; }
    public abstract void Use();
}
