using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// メインシーン・所持アイテム確認画面のキャンバスコントローラー
/// </summary>
public class CanvasController_ItemMenu : WindowBase
{
    [SerializeField] private Button _closeButton;
    
    public event Action OnHomeButtonClicked; // ホーム画面に戻る

    public override UniTask OnUIInitialize()
    {
        if (_closeButton != null) _closeButton.onClick.AddListener(BackHome);
        return base.OnUIInitialize();
    }

    /// <summary>
    /// モード選択画面→ホーム画面に遷移する
    /// </summary>
    private void BackHome() => OnHomeButtonClicked?.Invoke();

    private void OnDestroy()
    {
        if(_closeButton != null) _closeButton.onClick.RemoveAllListeners();
    }
}
