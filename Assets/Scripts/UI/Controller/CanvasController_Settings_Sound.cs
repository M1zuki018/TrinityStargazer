using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 設定画面・サウンド設定のキャンバスコントローラー
/// </summary>
public class CanvasController_Settings_Sound : WindowBase
{
    [SerializeField] private Button _closeButton;
    
    public event Action OnCloseButtonClicked;

    public override UniTask OnUIInitialize()
    {
        if(_closeButton != null) _closeButton.onClick.AddListener(OnCloseButtonClick);
        return base.OnUIInitialize();
    }
    
    private void OnCloseButtonClick() => OnCloseButtonClicked?.Invoke();

    private void OnDestroy()
    {
        if(_closeButton != null) _closeButton.onClick?.RemoveAllListeners();
    }
}
