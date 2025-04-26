using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// アイテム選択画面のキャンバスコントローラー
/// </summary>
public class CanvasController_ItemSelect : WindowBase
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _testItemButton;
    
    public event Action OnCloseButtonClicked;
    public event Action<ItemTypeEnum, RarityEnum, int> OnTestItemClicked;
    
    public override UniTask OnUIInitialize()
    {
        if(_closeButton != null) _closeButton.onClick.AddListener(OnCloseButtonClick);
        if(_testItemButton != null) _testItemButton.onClick.AddListener(OnTestItemClick);
        
        return base.OnUIInitialize();
    }

    /// <summary>
    /// パネルを閉じてバトル画面へ
    /// </summary>
    private void OnCloseButtonClick() => OnCloseButtonClicked?.Invoke();
    
    /// <summary>
    /// アイテムを使用した上でパネルを閉じる
    /// </summary>
    private void OnTestItemClick()
    {
        OnTestItemClicked?.Invoke(ItemTypeEnum.SealPage, RarityEnum.C, 1); // アイテムを追加
        OnCloseButtonClicked?.Invoke();
    }

    private void OnDestroy()
    {
        if(_closeButton != null) _closeButton.onClick.RemoveListener(OnCloseButtonClick);
        if(_testItemButton != null) _testItemButton.onClick.RemoveListener(OnTestItemClick);
    }
}
