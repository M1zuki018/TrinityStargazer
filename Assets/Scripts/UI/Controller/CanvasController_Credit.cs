using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// クレジット画面のキャンバスコントローラー
/// </summary>
public class CanvasController_Credit : WindowBase
{
    [SerializeField] private Button _closeButton;

    public event Action OnCloseButtonClicked;

    public override UniTask OnAwake()
    {
        if (_closeButton != null) _closeButton.onClick.AddListener(OpenCreditPanel);
        return base.OnAwake();
    }

    /// <summary>
    /// クレジットパネルを閉じるボタン
    /// </summary>
    private void OpenCreditPanel() => OnCloseButtonClicked?.Invoke();

    private void OnDestroy()
    {
        if (_closeButton != null) _closeButton.onClick?.RemoveAllListeners();
    }
}
