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
    [SerializeField] private RarityRate[] _currencyRates = new RarityRate[5]; // TODO: 後で難易度ごとに変更できるようにする

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
        // 通貨を獲得する処理
        int victoryPoints = GameManagerServiceLocator.Instance.VictoryPoints; // 勝利数
        GetCurrencyRewords(victoryPoints); // 獲得
        
        // アイテム報酬を用意する
        int baseItemCount = Mathf.Max(1, 1 + victoryPoints); // アイテムの獲得数のベース（最低1個 + 勝利数）
        int rand = Random.Range(0, 100); // 乱数を生成
        int itemCount = Mathf.Max(0, baseItemCount - rand); // アイテム獲得数のベースから乱数を引いて、残った分を報酬として提供する

        if (itemCount <= 0)
        {
            return; // アイテムを用意する必要がなければ以下の処理は行わない
        }
        
        var table = _rewordTable.GetRateTable(GameManagerServiceLocator.Instance.GetGameModeData().GameMode);
        for (int i = 0; i < itemCount; i++)
        {
            var rarity = RarityLottery(table.Item2);
            var item = ItemTypeLotteryWithValidRarity(table.Item1, rarity);
            InventoryManager.Instance.AddItem(item, rarity);
        }
    }

    /// <summary>
    /// 通貨報酬を獲得する
    /// </summary>
    private void GetCurrencyRewords(int victoryPoints)
    {
        // 勝利数に基づく通貨量の計算（ベース数 + 勝利数×2）
        int minimum = 1;
        int baseCurrencyAmount = minimum + (victoryPoints * 2);
        
        // 勝利数が多いほど高レアリティの通貨が出やすくなる補正
        float rarityBonus = Mathf.Min(0.5f, victoryPoints * 0.02f); // 最大50%のボーナス
        
        // 通貨のドロップ数範囲を取得
        Vector2Int countRange = new Vector2Int(1, 5);
        
        // 実際のドロップ数をランダムに決定
        int currencyDrops = Random.Range(countRange.x, countRange.y + 1);
        
        // 通貨ドロップの実行
        for (int i = 0; i < currencyDrops; i++)
        {
            // 勝利数による補正を適用した確率でレアリティを決定
            RarityEnum currencyRarity = CurrencyLottery(_currencyRates, rarityBonus);
            
            // 通貨量の計算（レアリティが高いほど少なくなる）
            int amount = CalculateCurrencyAmount(baseCurrencyAmount, currencyRarity);
            
            // 通貨を追加
            PlayerData.AddCurrency(currencyRarity, amount);
            Debug.Log($"通貨獲得：{currencyRarity} 量：{amount} PlayerData：{PlayerData.GetCurrency(currencyRarity)}");
        }
    }
    
    /// <summary>
    /// 通貨のレアリティを抽選する
    /// </summary>
    private RarityEnum CurrencyLottery(RarityRate[] currencyRates, float rarityBonus = 0f)
    {
        // 勝利ボーナスによる高レア補正
        float[] adjustedRates = new float[currencyRates.Length];
        float totalRate = 0f;
        
        // レートの補正計算
        for (int i = 0; i < currencyRates.Length; i++)
        {
            // 高レアリティほどボーナスの恩恵を受ける（低レアリティは逆に出にくくなる）
            float rarityModifier = 1f + ((float)i / (currencyRates.Length - 1) * 2 - 1) * rarityBonus;
            adjustedRates[i] = currencyRates[i].GetRate() * rarityModifier;
            totalRate += adjustedRates[i];
        }
        
        // 乱数でレアリティを決定
        float random = Random.Range(0f, totalRate);
        float cumulativeRate = 0f;
        
        for (int i = 0; i < currencyRates.Length; i++)
        {
            cumulativeRate += adjustedRates[i];
            if (random < cumulativeRate)
            {
                return currencyRates[i].GetRarity();
            }
        }
        
        // フォールバック（最低レアリティ）
        return RarityEnum.R;
    }
    
    /// <summary>
    /// 通貨量を計算する
    /// </summary>
    private int CalculateCurrencyAmount(int baseAmount, RarityEnum rarity)
    {
        // レアリティに応じた係数
        float[] rarityMultipliers = { 1.0f, 0.5f, 0.25f, 0.1f, 0.05f };
        int rarityIndex = (int)rarity;
        
        // 基本量 × レアリティ係数（端数は切り上げ）
        int amount = Mathf.CeilToInt(baseAmount * rarityMultipliers[rarityIndex]);
        
        // 最低保証量（少なくとも1個は入手できるようにする）
        return Mathf.Max(1, amount);
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
