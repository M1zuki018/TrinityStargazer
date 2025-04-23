using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ポーズ画面のキャンバスコントローラー
/// </summary>
public class CanvasController_Pause : WindowBase
{
    [SerializeField] private Button _resumeButton;
    [SerializeField] private Button _quitButton;
    
    public event Action OnResumeButtonClicked;
    public event Action OnQuitButtonClicked;

    public override UniTask OnUIInitialize()
    {
        if (_resumeButton != null) _resumeButton.onClick.AddListener(Resume);
        if (_quitButton != null) _quitButton.onClick.AddListener(Quit);
        return base.OnUIInitialize();
    }

    /// <summary>
    /// ポーズパネルを閉じる
    /// </summary>
    private void Resume() => OnResumeButtonClicked?.Invoke();
    
    /// <summary>
    /// ゲームをやめる
    /// </summary>
    private void Quit() => OnQuitButtonClicked?.Invoke();

    private void OnDestroy()
    {
        if(_resumeButton != null) _resumeButton.onClick?.RemoveAllListeners();
        if (_quitButton != null) _quitButton.onClick?.RemoveAllListeners();
    }
}
