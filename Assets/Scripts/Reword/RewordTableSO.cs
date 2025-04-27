using UnityEngine;

/// <summary>
/// バトルのアイテム報酬について定めるSO
/// </summary>
[CreateAssetMenu(fileName = "RewordTableSO", menuName = "Scriptable Objects/RewordTableSO")]
public class RewordTableSO : ScriptableObject
{
    [SerializeField] private RewordTable_GameMode[] _rewordTables = new RewordTable_GameMode[5];

    /// <summary>
    /// 報酬テーブルを返す
    /// </summary>
    public ItemRate[] GetRateTable(GameModeEnum mode)
    {
        return _rewordTables[(int)mode].GetRate();
    }
}
