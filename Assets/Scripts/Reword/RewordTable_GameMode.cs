using System;
using UnityEngine;

/// <summary>
/// ゲームモードごとのドロップ率
/// </summary>
[Serializable]
public class RewordTable_GameMode
{
    [SerializeField] private GameModeEnum _gameMode;
    [SerializeField] private ItemRate[] _gameModes = new ItemRate[7];

    /// <summary>
    /// レートを返す
    /// </summary>
    public ItemRate[] GetRate()
    {
        return _gameModes;
    }
}

