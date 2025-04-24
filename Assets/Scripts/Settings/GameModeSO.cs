using UnityEngine;

/// <summary>
/// ゲームモードのデータを管理するスクリプタブルオブジェクト
/// </summary>
[CreateAssetMenu(fileName = "GameModeSO", menuName = "Scriptable Objects/GameModeSO")]
public class GameModeSO : ScriptableObject
{
    [SerializeField] private ModeData[] _modeData = new ModeData[5];

    /// <summary>
    /// Modeデータを取得する
    /// </summary>
    public ModeData GetModeData(GameModeEnum mode)
    {
        return _modeData[(int)mode];
    }
}
