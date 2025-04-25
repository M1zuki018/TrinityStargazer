using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// モード選択画面のキャンバスコントローラー
/// </summary>
public class CanvasController_ModeSelect : WindowBase
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private List<ModeButton> _modeButtons = new List<ModeButton>();
    
    public event Action OnHomeButtonClicked; // ホーム画面に戻る
    public event Action OnGameModeButtonClicked; // インゲーム画面へ

    public override UniTask OnUIInitialize()
    {
        if(_closeButton != null) _closeButton.onClick.AddListener(BackHome);
        
        foreach (var modeButton in _modeButtons)
        {
            if (modeButton != null && modeButton.Button != null)
            {
                modeButton.Button.onClick.AddListener(() => SelectGameMode(modeButton.Mode));
            }
        }
        
        return base.OnUIInitialize();
    }

    /// <summary>
    /// モード選択画面→ホーム画面に遷移する
    /// </summary>
    private void BackHome()
    {
        OnHomeButtonClicked?.Invoke();
    }

    /// <summary>
    /// インゲームに遷移する
    /// </summary>
    private void SelectGameMode(GameModeEnum mode)
    {
        IGameManager.Instance.SetGameMode(mode);
        OnGameModeButtonClicked?.Invoke();
    }

    private void OnDestroy()
    {
        if(_closeButton != null) _closeButton.onClick.RemoveAllListeners();
        
        foreach (var modeButton in _modeButtons)
        {
            if(modeButton != null && modeButton.Button != null) 
                modeButton.Button.onClick.RemoveAllListeners();
        }
    }
}