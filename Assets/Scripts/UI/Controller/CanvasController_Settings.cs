using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// メインシーン・設定画面のキャンバスコントローラー
/// </summary>
public class CanvasController_Settings : WindowBase
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _graphicButton;
    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _environmentButton;
    [SerializeField] private Button _resetButton;
    
    public event Action OnHomeButtonClicked; // ホーム画面に戻る
    public event Action OnGraphicButtonClicked; // グラフィック設定
    public event Action OnSoundButtonClicked; // サウンド設定
    public event Action OnEnvironmentButtonClicked; // 環境設定
    public event Action OnResetButtonClicked; // データリセット

    public override UniTask OnUIInitialize()
    {
        if (_closeButton != null) _closeButton.onClick.AddListener(BackHome);
        if(_graphicButton != null) _graphicButton.onClick.AddListener(OpenGraphic);
        if(_soundButton != null) _soundButton.onClick.AddListener(OpenSound);
        if(_environmentButton != null) _environmentButton.onClick.AddListener(OpenEnvironment);
        if(_resetButton != null) _resetButton.onClick.AddListener(OpenReset);
        return base.OnUIInitialize();
    }
    
    /// <summary>
    /// モード選択画面→ホーム画面に遷移する
    /// </summary>
    private void BackHome() => OnHomeButtonClicked?.Invoke();

    /// <summary>
    /// グラフィック設定を開く
    /// </summary>
    private void OpenGraphic() => OnGraphicButtonClicked?.Invoke();
    
    /// <summary>
    /// サウンド設定を開く
    /// </summary>
    private void OpenSound() => OnSoundButtonClicked?.Invoke();
    
    /// <summary>
    /// 環境設定を開く
    /// </summary>
    private void OpenEnvironment() => OnEnvironmentButtonClicked?.Invoke();
    
    /// <summary>
    /// データリセットパネルを開く
    /// </summary>
    private void OpenReset() => OnResetButtonClicked?.Invoke();

    private void OnDestroy()
    {
        if(_closeButton != null) _closeButton.onClick.RemoveAllListeners();
        if(_graphicButton != null) _graphicButton.onClick.RemoveAllListeners();
        if(_soundButton != null) _soundButton.onClick.RemoveAllListeners();
        if(_environmentButton != null) _environmentButton.onClick.RemoveAllListeners();
        if(_resetButton != null) _resetButton.onClick.RemoveAllListeners();
    }
}
