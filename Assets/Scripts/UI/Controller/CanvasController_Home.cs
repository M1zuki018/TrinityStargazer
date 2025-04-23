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
    
    public event Action OnStartButtonClicked; // モード選択へ
    public event Action OnShopButtonClicked; // ショップへ
    public event Action OnItemButtonClicked; // アイテム画面へ
    public event Action OnSettingsButtonClicked; // 設定画面へ
    
    public override UniTask OnUIInitialize()
    {
        if (_startButton != null) _startButton.onClick.AddListener(GameStart);
        if (_shopButton != null) _shopButton.onClick.AddListener(GoShop);
        if (_itemButton != null) _itemButton.onClick.AddListener(GoItem); 
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
            int playerLevel = PlayerData.Level;
            _levelText.text = $"Lv. {playerLevel}";
        }
    }
    
    /// <summary>
    /// ホーム画面→ゲームモード選択画面に遷移する
    /// </summary>
    private void GameStart()
    {
        OnStartButtonClicked?.Invoke();
        GameManager.Instance.ChangeGameState(GameStateEnum.ModeSelect);
    }

    /// <summary>
    /// ホーム画面→ショップ画面に遷移する
    /// </summary>
    private void GoShop()
    {
        OnShopButtonClicked?.Invoke();
        GameManager.Instance.ChangeGameState(GameStateEnum.Shop);
    }
    
    /// <summary>
    /// ホーム画面→アイテム画面に遷移する
    /// </summary>
    private void GoItem()
    {
        OnItemButtonClicked?.Invoke();
        GameManager.Instance.ChangeGameState(GameStateEnum.Menu);
    }
    
    /// <summary>
    /// ホーム画面→設定画面に遷移する
    /// </summary>
    private void GoSettings()
    {
        OnSettingsButtonClicked?.Invoke();
        GameManager.Instance.ChangeGameState(GameStateEnum.Settings);
    }

    private void OnDestroy()
    {
        _startButton.onClick?.RemoveAllListeners();
        _shopButton.onClick?.RemoveAllListeners();
        _itemButton.onClick?.RemoveAllListeners();
        _settingsButton.onClick?.RemoveAllListeners();
    }
}
