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
    
    [SerializeField] private ItemValidRarities[] _itemValidRarities;
    
    /// <summary>
    /// 報酬テーブルを返す
    /// </summary>
    public (ItemRate[],RarityRate[]) GetRateTable(GameModeEnum mode)
    {
        return (_rewordTables[(int)mode].GetRate(), _rewordTables[(int)mode].GetRarityRate());
    }
    
    /// <summary>
    /// 指定された勝利数による保証レアリティを取得
    /// </summary>
    public RarityEnum? GetGuaranteedRarity(int victoryCount)
    {
        foreach (var guarantee in _victoryCountGuarantees)
        {
            if (victoryCount >= guarantee.GetRequiredVictoryCount())
            {
                return guarantee.GetGuaranteedRarity();
            }
        }
        return null;
    }
    
    /// <summary>
    /// アイテムタイプに指定されたレアリティが有効かチェック
    /// </summary>
    public bool IsValidRarity(ItemTypeEnum itemType, RarityEnum rarity)
    {
        var validRarities = GetValidRaritiesForItem(itemType);
        foreach (var validRarity in validRarities)
        {
            if (validRarity == rarity)
            {
                return true;
            }
        }
        return false;
    }
    
    /// <summary>
    /// アイテムタイプに有効なレアリティリストを取得
    /// </summary>
    public RarityEnum[] GetValidRaritiesForItem(ItemTypeEnum itemType)
    {
        foreach (var item in _itemValidRarities)
        {
            if (item.ItemType == itemType)
            {
                return item.ValidRarities;
            }
        }
        
        // デフォルトでは全てのレアリティが有効とする
        return new[] { RarityEnum.N, RarityEnum.C, RarityEnum.R, RarityEnum.SR, RarityEnum.SSR };
    }
}
