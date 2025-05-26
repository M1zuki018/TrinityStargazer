using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤーの名前などを設定するパネルのキャンバスコントローラー
/// </summary>
public class CanvasController_PlayerData : WindowBase
{
    [SerializeField] private NameSetting _nameSetting;
    [SerializeField] private Button _closeButton;
    
    public event Action OnCloseButtonClicked;

    public override UniTask OnUIInitialize()
    {
        if(_closeButton != null) _closeButton.onClick.AddListener(HandleCloseButtonClicked);
        return base.OnUIInitialize();
    }
    
    /// <summary>
    /// パネルを閉じる
    /// </summary>
    private void HandleCloseButtonClicked() => OnCloseButtonClicked?.Invoke();

    private void OnDestroy()
    {
        if(_closeButton != null) _closeButton.onClick.RemoveAllListeners();
    }
}
