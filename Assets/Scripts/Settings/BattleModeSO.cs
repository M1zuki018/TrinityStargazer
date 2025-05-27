using UnityEngine;

/// <summary>
/// ゲームモードのデータを管理するスクリプタブルオブジェクト
/// </summary>
[CreateAssetMenu(fileName = "BattleModeSO", menuName = "Scriptable Objects/BattleModeSO")]
public class BattleModeSO : ScriptableObject
{
    [SerializeField] private BattleModeData[] _modeData = new BattleModeData[5];

    /// <summary>
    /// Modeデータを取得する
    /// </summary>
    public BattleModeData GetModeData(GameModeEnum mode)
    {
        return _modeData[(int)mode];
    }
}
