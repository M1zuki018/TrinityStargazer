using System;
using UnityEngine;

/// <summary>
/// アイテムごとに有効なレアリティを定義する
/// </summary>
[Serializable]
public class ItemValidRarities
{
    [SerializeField] private ItemTypeEnum _itemType;
    [SerializeField] private RarityEnum[] _validRarities;
    
    public ItemTypeEnum ItemType => _itemType;
    public RarityEnum[] ValidRarities => _validRarities;
}