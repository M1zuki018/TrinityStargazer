using UnityEngine;

/// <summary>
/// ゲームモードのデータ
/// </summary>
[System.Serializable]
public struct ModeData
{
    [SerializeField] private GameModeEnum _gameMode;
    [SerializeField] private string _gameModeName;
    [SerializeField] private int _maxTurn;
    
    public GameModeEnum GameMode => _gameMode;
    public string GameModeName => _gameModeName;
    public int MaxTurn => _maxTurn;
}
