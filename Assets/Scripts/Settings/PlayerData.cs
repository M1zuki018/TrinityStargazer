using System.Collections.Generic;

/// <summary>
/// プレイヤー情報を保持しておくための静的クラス
/// </summary>
public static class PlayerData
{
    public static int Level { get; } = 1;

    // ゲーム内通貨のレアリティごとのディクショナリ
    private static Dictionary<RarityEnum, int> _currencies = new Dictionary<RarityEnum, int>()
    {
        { RarityEnum.R, 0 },
        { RarityEnum.SR, 0 },
        { RarityEnum.SSR, 0 },
    };

    /// <summary>
    /// 指定した通貨を追加する
    /// </summary>
    public static void AddCurrency(RarityEnum rarity, int amount)
    {
        if (_currencies.ContainsKey(rarity))
        {
            _currencies[rarity] += amount;
        }
    }
    
    /// <summary>
    /// 指定した通貨の所持数を取得
    /// </summary>
    public static int GetCurrency(RarityEnum rarity)
    {
        if (_currencies.ContainsKey(rarity))
        {
            return _currencies[rarity];
        }
        return 0;
    }
    
    /// <summary>
    /// 指定した通貨を消費する。不足時はfalseを返す
    /// </summary>
    public static bool ConsumeCurrency(RarityEnum rarity, int amount)
    {
        if (_currencies.ContainsKey(rarity) && _currencies[rarity] >= amount)
        {
            _currencies[rarity] -= amount;
            
            return true;
        }
        return false;
    }
    
    /// <summary>
    /// 通貨を合成する（下位→上位）
    /// デフォルトでは3つ消費で上位のアイテムに合成
    /// </summary>
    public static bool SynthesizeCurrency(RarityEnum fromRarity, RarityEnum toRarity, int conversionRate = 3)
    {
        // 上位通貨への合成のみ許可
        if ((int)toRarity <= (int)fromRarity)
            return false;
        
        // 1段階上の通貨への合成のみサポート
        if ((int)toRarity != (int)fromRarity + 1)
            return false;
        
        if (_currencies.ContainsKey(fromRarity) && _currencies[fromRarity] >= conversionRate)
        {
            _currencies[fromRarity] -= conversionRate;
            _currencies[toRarity] += 1;
            
            return true;
        }
        return false;
    }
}
