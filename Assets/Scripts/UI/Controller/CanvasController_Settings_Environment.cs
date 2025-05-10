using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 設定画面・環境設定のキャンバスコントローラー
/// </summary>
public class CanvasController_Settings_Environment : WindowBase
{
    [SerializeField] private Button _closeButton;
    
    [Header("言語")]
    [SerializeField] private Button _textJapanese;
    [SerializeField] private Button _textEnglish;
    [SerializeField] private Button _voiseJapanese;
    [SerializeField] private Button _voiseEnglish;

    [Header("シナリオ")]
    [SerializeField] private Button _scenarioSpeedLeft;
    [SerializeField] private Button _scenarioSpeedRight;
    [SerializeField] private Button _useAute;
    [SerializeField] private Button _donnotUseAute;
    
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
