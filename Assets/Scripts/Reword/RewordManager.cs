using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// バトル報酬を管理するクラス
/// </summary>
public class RewordManager : ViewBase
{
    [SerializeField] private BattleSystemPresenter _battleSystemPresenter;
    [SerializeField][ExpandableSO] private RewordTableSO _rewordTable;

    public override UniTask OnAwake()
    {
        _battleSystemPresenter.OnBattleCompleted += GetRewords;
        return base.OnAwake();
    }
    
    /// <summary>
    /// 報酬を用意する
    /// </summary>
    private void GetRewords()
    {
        int count = 5; // TODO: 個数指定を追加
        // 現在のゲームモードの報酬テーブルを取得
        var table = _rewordTable.GetRateTable(GameManagerServiceLocator.Instance.GetGameModeData().GameMode);

        // 勝利数に基づく保証レアリティを確認
        RarityEnum? guaranteedRarity = _rewordTable.GetGuaranteedRarity(GameManagerServiceLocator.Instance.VictoryPoints);
        
        for (int i = 0; i < count; i++)
        {
            // 最初のアイテムに保証レアリティを適用
            if (i == 0 && guaranteedRarity.HasValue)
            {
                // 保証レアリティに対応するアイテムを抽選
                var item = ItemTypeLotteryWithValidRarity(table.Item1, guaranteedRarity.Value);
                InventoryManager.Instance.AddItem(item, guaranteedRarity.Value);
            }
            else
            {
                // 通常の抽選
                var rarity = RarityLottery(table.Item2);
                // このレアリティが有効なアイテムのみで抽選
                var item = ItemTypeLotteryWithValidRarity(table.Item1, rarity);
                InventoryManager.Instance.AddItem(item, rarity);
            }
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

    // このレアリティが有効なアイテムタイプのみで抽選を行う
    private ItemTypeEnum ItemTypeLotteryWithValidRarity(ItemRate[] itemRates, RarityEnum rarity)
    {
        // このレアリティが有効なアイテムタイプとその確率を再計算
        var validItemRates = new List<(ItemTypeEnum, float)>();
        float totalValidRate = 0f;
    
        foreach (var rate in itemRates)
        {
            if (_rewordTable.IsValidRarity(rate.ItemType, rarity))
            {
                validItemRates.Add((rate.ItemType, rate.GetRate()));
                totalValidRate += rate.GetRate();
            }
        }
    
        // 有効なアイテムがない場合のフォールバック
        if (validItemRates.Count == 0)
        {
            Debug.LogWarning($"No valid items found for rarity {rarity}. Using fallback.");
            return ItemTypeEnum.CelestialForecast; // フォールバックアイテム
        }
    
        // 確率の正規化（合計が1になるよう調整）
        if (totalValidRate > 0)
        {
            for (int i = 0; i < validItemRates.Count; i++)
            {
                var item = validItemRates[i];
                validItemRates[i] = (item.Item1, item.Item2 / totalValidRate);
            }
        }
    
        // 抽選実行
        float rnd = Random.Range(0f, 1f);
        float cumulative = 0f;
    
        foreach (var item in validItemRates)
        {
            cumulative += item.Item2;
            if (rnd <= cumulative)
            {
                return item.Item1;
            }
        }
    
        // ここに到達することはほぼないはずだが、安全のため
        return validItemRates[validItemRates.Count - 1].Item1;
    }

    private void OnDestroy()
    {
        _battleSystemPresenter.OnBattleCompleted -= GetRewords;
    }
}
