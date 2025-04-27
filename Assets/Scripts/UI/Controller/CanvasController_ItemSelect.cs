using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// アイテム選択画面のキャンバスコントローラー
/// </summary>
public class CanvasController_ItemSelect : WindowBase
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private InventoryManager _inventoryManager;
    [SerializeField] private GameObject _itemButtonPrefab;
    [SerializeField] private Transform _itemButtonParent;
    private List<ButtleItemButton> _buttons = new List<ButtleItemButton>(10);
        
    public event Action OnCloseButtonClicked;
    public event Action<ItemTypeEnum, RarityEnum, int> OnTestItemClicked;
    
    public override UniTask OnUIInitialize()
    {
        _inventoryManager.OnInventoryChanged += CreateItemButton;
        
        if(_closeButton != null) _closeButton.onClick.AddListener(OnCloseButtonClick);

        // 既に持っている分のアイテムを生成する
        foreach (var items in InventoryManager.Instance.Inventory)
        {
            CreateItemButton(items.Key.Item1, items.Key.Item2, items.Value);
        }
        
        return base.OnUIInitialize();
    }

    /// <summary>
    /// パネルを閉じてバトル画面へ
    /// </summary>
    private void OnCloseButtonClick() => OnCloseButtonClicked?.Invoke();
    
    /// <summary>
    /// アイテムを使用した上でパネルを閉じる
    /// </summary>
    public void OnTestItemClick(ItemTypeEnum itemType, RarityEnum rarity)
    {
        OnTestItemClicked?.Invoke(itemType, rarity, 1); // アイテムを追加
        OnCloseButtonClicked?.Invoke();
    }

    /// <summary>
    /// Inventoryが変化した時にアイテムボタンを生成する
    /// </summary>
    private void CreateItemButton(ItemTypeEnum itemType, RarityEnum rarity, int count)
    {
        ButtleItemButton button = Instantiate(_itemButtonPrefab, _itemButtonParent).GetComponent<ButtleItemButton>();
        button.Initialize(itemType, rarity, this); // ボタンの表示の初期化を行う
    }

    /// <summary>
    /// リストから自身を削除する
    /// </summary>
    public void RemoveList(ButtleItemButton button)
    {
        _buttons.Remove(button);
    }

    private void OnDestroy()
    {
        if(_closeButton != null) _closeButton.onClick.RemoveListener(OnCloseButtonClick);
    }
}
