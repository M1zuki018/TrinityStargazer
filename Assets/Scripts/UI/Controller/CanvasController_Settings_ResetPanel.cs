using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 設定画面・リセットパネルのキャンバスコントローラー
/// </summary>
public class CanvasController_Settings_ResetPanel : WindowBase
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _yesButton;
    [SerializeField] private Button _noButton;
    
    public event Action OnButtonClicked;
    
    public override UniTask OnUIInitialize()
    {
        if(_closeButton != null) _closeButton.onClick.AddListener(Close);
        if(_yesButton != null) _yesButton.onClick.AddListener(DataReset);
        if(_noButton != null) _noButton.onClick.AddListener(Close);
        return base.OnUIInitialize();
    }
    
    private void Close() => OnButtonClicked?.Invoke();

    /// <summary>
    /// セーブデータのリセット処理
    /// </summary>
    private void DataReset()
    {
        Close(); // リセット画面を閉じる
        Debug.Log("TODO:データリセット処理を作る");
    }

    private void OnDestroy()
    {
        if(_closeButton != null) _closeButton.onClick?.RemoveAllListeners();
        if(_yesButton != null) _yesButton.onClick?.RemoveAllListeners();
        if(_noButton != null) _noButton.onClick?.RemoveAllListeners();
    }
}
