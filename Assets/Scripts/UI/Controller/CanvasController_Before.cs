using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// BeforePanelのキャンバスコントローラー
/// </summary>
public class CanvasController_Before : WindowBase
{
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _battleButton;
    [SerializeField] private Button _itemSelectButton;
    [SerializeField] private Button _chatButton;
    
    public event Action OnPauseButtonClicked;
    public event Action OnBattleButtonClicked;
    public event Action OnItemSelectButtonClicked;
    public event Action OnChatButtonClicked;

    public override UniTask OnUIInitialize()
    {
        if (_pauseButton != null) _pauseButton.onClick.AddListener(OpenPausePanel);
        if (_battleButton != null) _battleButton.onClick.AddListener(OnBattleButtonClick);
        if (_itemSelectButton != null) _itemSelectButton.onClick.AddListener(OnItemSelectButtonClick);
        if (_chatButton != null) _chatButton.onClick.AddListener(OnChatButtonClick);
        
        return base.OnUIInitialize();
    }

    /// <summary>
    /// 方向選択パネルを開く
    /// </summary>
    private void OnBattleButtonClick() => OnBattleButtonClicked?.Invoke();

    /// <summary>
    /// アイテム選択パネルを開く
    /// </summary>
    private void OnItemSelectButtonClick() => OnItemSelectButtonClicked?.Invoke();

    /// <summary>
    /// チャットパネルを開く
    /// </summary>
    private void OnChatButtonClick() => OnChatButtonClicked?.Invoke();
    
    /// <summary>
    /// ポーズパネルを開く
    /// </summary>
    private void OpenPausePanel() => OnPauseButtonClicked?.Invoke();

    private void OnDestroy()
    {
        if(_pauseButton != null) _pauseButton.onClick?.RemoveAllListeners();
        if (_battleButton != null) _battleButton.onClick?.RemoveAllListeners();
        if (_itemSelectButton != null) _itemSelectButton.onClick?.RemoveAllListeners();
        if (_chatButton != null) _chatButton.onClick?.RemoveAllListeners();
    }
}