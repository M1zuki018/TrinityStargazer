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
    /// レートを取得する
    /// </summary>
    public float GetRate()
    {
        return _rate;
    }
}
