using UnityEngine;

/// <summary>
/// バトル報酬を管理するクラス
/// </summary>
public class RewordManager : ViewBase
{
    [SerializeField][ExpandableSO] private RewordTableSO _rewordTable;

    /// <summary>
    /// 報酬を用意する
    /// </summary>
    public void GetRewords(int winCount)
    {
        int count = 2; // TODO: 個数指定を追加
        // 現在のゲームモードの報酬テーブルを取得
        var table = _rewordTable.GetRateTable(GameManagerServiceLocator.Instance.GetGameModeData().GameMode);

        for (int i = 0; i < count; i++)
        {
            var rarity = RarityLottery(table.Item2);
            var item = ItemTypeLottery(table.Item1);
            InventoryManager.Instance.AddItem(item, rarity);
        }
    }

    /// <summary>
    /// レアリティを決める抽選
    /// </summary>
    private RarityEnum RarityLottery(RarityRate[] rarityRates)
    {
        int you = Random.Range(0, 100);
        float sum = 0;
        
        foreach (var rarity in rarityRates)
        {
            sum += rarity.GetRate();
            if (sum > you)
            {
                return rarity.GetRarity();
            }
        }

        return RarityEnum.SSR;
    }

    /// <summary>
    /// アイテムの種類を決める抽選
    /// </summary>
    private ItemTypeEnum ItemTypeLottery(ItemRate[] itemRates)
    {
        return ItemTypeEnum.CelestialForecast; //TODO:  仮
    }
}
