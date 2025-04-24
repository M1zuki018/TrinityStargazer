using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテムを管理するクラス用のインターフェース
/// </summary>
public interface IItemManager
{
    public event Action<DirectionEnum, float> UseModifyDirectionProbabilityItem;

}
