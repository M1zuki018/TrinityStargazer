using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// アイテムのInventoryを管理するクラス
/// </summary>
public class InventoryManager : ViewBase
{
    public static InventoryManager Instance;
    
    // (アイテムの種類, レアリティ) 個数のkvp
    private Dictionary<(ItemTypeEnum, RarityEnum), int> _inventory = new Dictionary<(ItemTypeEnum, RarityEnum), int>();
    private Dictionary<ItemTypeEnum, int> _maxCapacity = new Dictionary<ItemTypeEnum, int>(); // 最大所持数
    public event Action<ItemTypeEnum, RarityEnum, int> OnInventoryChanged; // アイテム変更時のイベント (アイテムタイプ, レアリティ, 新しい数量)
    
    public override UniTask OnAwake()
    {
        // シングルトンパターンの実装
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            InitializeMaxCapacity();
        }
        else
        {
            Destroy(gameObject);
        }
        
        return base.OnAwake();
    }

    /// <summary>
    /// アイテムタイプごとの最大所持数を初期化
    /// </summary>
    private void InitializeMaxCapacity()
    {
        foreach (ItemTypeEnum itemType in Enum.GetValues(typeof(ItemTypeEnum)))
        {
            _maxCapacity[itemType] = 99; // デフォルト値
        }
        // TODO: 特別な設定があればここで上書きする
    }

    [ContextMenu("ItemUse")]
    private void ItemUseTest()
    {
        AddItem(ItemTypeEnum.SealPage, RarityEnum.C);
        UseItem(ItemTypeEnum.SealPage, RarityEnum.C);
    }

    /// <summary>
    /// Inventoryにアイテムを追加する
    /// </summary>
    public bool AddItem(ItemTypeEnum itemType, RarityEnum rarity, int amount = 1)
    {
        if (!_inventory.ContainsKey((itemType, rarity)))
        {
            _inventory[(itemType, rarity)] = 0;
        }
        
        // 最大所持数チェック
        if (_inventory[(itemType, rarity)] + amount > _maxCapacity[itemType])
        {
            Debug.LogWarning($"{itemType}の最大所持数({_maxCapacity[itemType]})を超えるため追加できません");
            return false;
        }
        
        _inventory[(itemType, rarity)] += amount;
        Debug.Log("Add");
        
        return true;
    }

    /// <summary>
    /// Inventoryからアイテムを使う
    /// </summary>
    public bool UseItem(ItemTypeEnum itemType, RarityEnum rarity, int amount = 1)
    {
        // キーが存在しない場合は追加
        if (!_inventory.ContainsKey((itemType, rarity)))
        {
            _inventory[(itemType, rarity)] = 0;
            return false;
        }
        
        if (_inventory[(itemType, rarity)] < amount)
        {
            Debug.LogWarning($"{itemType}(レアリティ{rarity})の在庫が足りません。必要:{amount}, 所持:{_inventory[(itemType, rarity)]}");
            return false;
        }
        
        _inventory[(itemType, rarity)] -= amount;
        
        // イベント発火
        OnInventoryChanged?.Invoke(itemType, rarity, _inventory[(itemType, rarity)]);
        Debug.Log("Use");
        
        return true;
    }

    /// <summary>
    /// 特定のアイテムの所持数を取得
    /// </summary>
    public int GetItemCount(ItemTypeEnum itemType, RarityEnum rarity)
    {
        if (!_inventory.ContainsKey((itemType, rarity)))
        {
            return 0;
        }
        
        return _inventory[(itemType, rarity)];
    }

    /// <summary>
    /// 特定のアイテムタイプの所持数合計を取得（全レアリティ）
    /// </summary>
    public int GetTotalItemCountByType(ItemTypeEnum itemType)
    {
        int total = 0;
        
        foreach (var kvp in _inventory)
        {
            if (kvp.Key.Item1 == itemType)
            {
                total += kvp.Value;
            }
        }
        
        return total;
    }
    
    /// <summary>
    /// Inventoryを全てクリアする
    /// </summary>
    public void ResetInventory()
    {
        var oldInventory = new Dictionary<(ItemTypeEnum, RarityEnum), int>(_inventory);
        _inventory.Clear();
        
        // 全アイテムについてイベント発火
        foreach (var kvp in oldInventory)
        {
            if (kvp.Value > 0)
            {
                OnInventoryChanged?.Invoke(kvp.Key.Item1, kvp.Key.Item2, 0);
            }
        }
    }
}
