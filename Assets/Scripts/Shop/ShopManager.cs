/// <summary>
/// ショップ機能を管理するマネージャークラス
/// </summary>
public class ShopManager : ViewBase
{
    /// <summary>
    /// 指定した通貨を消費する。不足時はfalseを返す
    /// </summary>
    public bool ConsumeCurrency(RarityEnum rarity, int amount)
    {
        return PlayerData.DecreaseCurrency(rarity, amount);
    }
    
    /// <summary>
    /// 通貨を合成する（下位→上位）
    /// デフォルトでは3つ消費で上位のアイテムに合成
    /// </summary>
    public bool SynthesizeCurrency(RarityEnum fromRarity, RarityEnum toRarity, int conversionRate = 3)
    {
        // 上位通貨への合成のみ許可
        if ((int)toRarity <= (int)fromRarity)
            return false;
        
        // 1段階上の通貨への合成のみサポート
        if ((int)toRarity != (int)fromRarity + 1)
            return false;
        
        if (PlayerData.GetCurrency(fromRarity) >= conversionRate)
        {
            if (PlayerData.DecreaseCurrency(fromRarity, conversionRate))
            {
                PlayerData.AddCurrency(toRarity, 1);
                return true;
            }
        }
        return false;
    }
}
