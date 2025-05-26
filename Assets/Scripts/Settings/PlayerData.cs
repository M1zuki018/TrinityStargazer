using System.Collections.Generic;
using UniRx;

/// <summary>
/// プレイヤー情報を保持しておくための静的クラス
/// </summary>
public static class PlayerData
{
    public static ReactiveProperty<string> NameProp = new ReactiveProperty<string>("Default");
    public static ReactiveProperty<int> LevelProp = new ReactiveProperty<int>(1);

    // ゲーム内通貨のレアリティごとのディクショナリ
    private static Dictionary<RarityEnum, int> _currencies = new Dictionary<RarityEnum, int>()
    {
        { RarityEnum.R, 0 },
        { RarityEnum.SR, 0 },
        { RarityEnum.SSR, 0 },
    };
    
    /// <summary>
    /// 名前を設定する
    /// </summary>
    public static void SetName(string name) => NameProp.Value = name;
    
    /// <summary>
    /// レベルを設定する
    /// </summary>
    public static void SetLevel(int value) => LevelProp.Value = value;
    
    /// <summary>
    /// レベルアップ
    /// </summary>
    public static void LevelUp() => LevelProp.Value++;
    
    /// <summary>
    /// レベルリセット
    /// </summary>
    public static void LevelReset() => LevelProp.Value = 1;
    
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
    /// 通貨を消費する
    /// </summary>
    public static bool DecreaseCurrency(RarityEnum rarity, int amount)
    {
        if (_currencies.ContainsKey(rarity) && _currencies[rarity] >= amount)
        {
            _currencies[rarity] -= amount;
            return true;
        }
        return false;
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
}
