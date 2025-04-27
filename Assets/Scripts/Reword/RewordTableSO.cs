using UnityEngine;

/// <summary>
/// バトルのアイテム報酬について定めるSO
/// </summary>
[CreateAssetMenu(fileName = "RewordTableSO", menuName = "Scriptable Objects/RewordTableSO")]
public class RewordTableSO : ScriptableObject
{
    [Header("アイテムタイプ出現率テーブル")]
    [SerializeField] private RewordTable_GameMode[] _rewordTables = new RewordTable_GameMode[5];
    
    [Header("勝利数に基づくレアリティ保証設定")]
    [SerializeField] private VictoryCountRarityGuarantee[] _victoryCountGuarantees;
    
    /// <summary>
    /// 報酬テーブルを返す
    /// </summary>
    public (ItemRate[],RarityRate[]) GetRateTable(GameModeEnum mode)
    {
        return (_rewordTables[(int)mode].GetRate(), _rewordTables[(int)mode].GetRarityRate());
    }
}
