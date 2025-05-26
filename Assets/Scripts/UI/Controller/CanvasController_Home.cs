using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ホーム画面のキャンバスコントローラー
/// </summary>
public class CanvasController_Home : WindowBase
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _shopButton;
    [SerializeField] private Button _itemButton;
    [SerializeField] private Button _settingsButton;
    [SerializeField] private Text _levelText;
    
    public event Action OnModeSelectButtonClicked; // モード選択へ
    public event Action OnShopButtonClicked; // ショップへ
    public event Action OnItemMenuButtonClicked; // アイテム画面へ
    public event Action OnSettingsButtonClicked; // 設定画面へ
    
    public override UniTask OnUIInitialize()
    {
        if (_startButton != null) _startButton.onClick.AddListener(GoModeSelect);
        if (_shopButton != null) _shopButton.onClick.AddListener(GoShop);
        if (_itemButton != null) _itemButton.onClick.AddListener(GoItemMenu); 
        if (_settingsButton != null) _settingsButton.onClick.AddListener(GoSettings);
    
        UpdateLevelText();
        
        return base.OnUIInitialize();
    }
    
    /// <summary>
    /// レベル表示を更新する
    /// </summary>
    private void UpdateLevelText()
    {
        if (_levelText != null)
        {
            _levelText.text = $"{PlayerData.Name} Lv. {PlayerData.Level}";
        }
    }
    
    /// <summary>
    /// ホーム画面→ゲームモード選択画面に遷移する
    /// </summary>
    private void GoModeSelect() => OnModeSelectButtonClicked?.Invoke();

    /// <summary>
    /// ホーム画面→ショップ画面に遷移する
    /// </summary>
    private void GoShop() => OnShopButtonClicked?.Invoke();
    
    /// <summary>
    /// ホーム画面→アイテム画面に遷移する
    /// </summary>
    private void GoItemMenu() => OnItemMenuButtonClicked?.Invoke();
    
    /// <summary>
    /// ホーム画面→設定画面に遷移する
    /// </summary>
    private void GoSettings() => OnSettingsButtonClicked?.Invoke(); 

    private void OnDestroy()
    {
        if (_startButton != null) _startButton.onClick?.RemoveAllListeners();
        if (_shopButton != null) _shopButton.onClick?.RemoveAllListeners();
        if (_itemButton != null) _itemButton.onClick?.RemoveAllListeners();
        if (_settingsButton != null) _settingsButton.onClick?.RemoveAllListeners();
    }

    public override void Show()
    {
        GlobalFadePanel.RequestFadeIn(1f);
        base.Show();
    }
}
