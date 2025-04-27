using System;
using UnityEngine;

/// <summary>
/// ゲームモードごとのドロップ率
/// </summary>
[Serializable]
public class RewordTable_GameMode
{
    [SerializeField] private GameModeEnum _gameMode;
    [Header("アイテムごとの出現率")]
    [SerializeField] private ItemRate[] _gameModes = new ItemRate[7];
    [Header("レアリティごとの出現率")]
    [SerializeField] private RarityRate[] _rarityRates = new RarityRate[5];

    /// <summary>
    /// アイテムごとのレートを返す
    /// </summary>
    public ItemRate[] GetRate()
    {
        return _gameModes;
    }

    /// <summary>
    /// レアリティごとのレートを返す
    /// </summary>
    public RarityRate[] GetRarityRate()
    {
        return _rarityRates;
    }
}

