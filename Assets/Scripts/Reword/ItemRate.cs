using System;
using UnityEngine;

/// <summary>
/// ItemTypeEnumとレートのセット
/// </summary>
[Serializable]
public class ItemRate
{
    [SerializeField] private ItemTypeEnum _itemType;
    [SerializeField] private float _rate;

    /// <summary>
    /// アイテムタイプを取得する
    /// </summary>
    public ItemTypeEnum ItemType => _itemType;
    
    /// <summary>
    /// レートを取得する
    /// </summary>
    public float GetRate()
    {
        return _rate;
    }
}
