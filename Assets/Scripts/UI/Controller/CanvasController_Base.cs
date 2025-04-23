using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// バトルのベース画面のキャンバスコントローラー
/// </summary>
public class CanvasController_Base : WindowBase
{
    [SerializeField] private Button _pauseButton;
    
    public event Action OnPauseButtonClicked;

    public override UniTask OnUIInitialize()
    {
        if (_pauseButton != null) _pauseButton.onClick.AddListener(OpenPausePanel);
        return base.OnUIInitialize();
    }

    /// <summary>
    /// ポーズパネルを開く
    /// </summary>
    private void OpenPausePanel() => OnPauseButtonClicked?.Invoke();

    private void OnDestroy()
    {
        if(_pauseButton != null) _pauseButton.onClick?.RemoveAllListeners();
    }
}
