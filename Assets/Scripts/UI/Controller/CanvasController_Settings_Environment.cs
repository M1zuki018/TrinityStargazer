using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// 設定画面・環境設定のキャンバスコントローラー
/// </summary>
public class CanvasController_Settings_Environment : WindowBase
{
    [SerializeField] private Button _closeButton;

    [Header("言語")] 
    [SerializeField] private Button[] _textLanguage;
    [SerializeField] private Button[] _voiceLanguage;

    [Header("シナリオ")]
    [SerializeField] private Button _scenarioSpeedLeft;
    [SerializeField] private Button _scenarioSpeedRight;
    [SerializeField] private Button _useAute;
    [SerializeField] private Button _doNotUseAuto;
    
    private LanguageSetting _languageSetting;
    
    public event Action OnCloseButtonClicked;
    public event Action<LanguageEnum> OnTextLanguageChanged; // テキスト言語を変更
    public event Action<LanguageEnum> OnVoiceLanguageChanged; // ボイス言語を変更
    public event Action<int> OnScenarioSpeedChanged; // シナリオ再生速度を変更
    public event Action<bool> OnUseAutoChanged; // オート再生をするか変更する
    

    public override UniTask OnUIInitialize()
    {
        if(_closeButton != null) _closeButton.onClick.AddListener(OnCloseButtonClick);
        if(_scenarioSpeedLeft != null) _scenarioSpeedLeft.onClick.AddListener(() => ScenarioSpeedChanged(-1));
        if(_scenarioSpeedRight != null) _scenarioSpeedRight.onClick.AddListener(() => ScenarioSpeedChanged(1));
        if(_useAute != null) _useAute.onClick.AddListener(() => ScenarioAutoChanged(true));
        if(_doNotUseAuto != null) _doNotUseAuto.onClick.AddListener(() => ScenarioAutoChanged(false));
        RegisterLanguageButtons(_textLanguage, true);
        RegisterLanguageButtons(_voiceLanguage, false);

        _languageSetting = new LanguageSetting(this);
        
        return base.OnUIInitialize();
    }
    
    private void RegisterLanguageButtons(Button[] buttons, bool isTextLanguageChanged)
    {
        if (buttons == null) return;
        
        for (int i = 0; i < buttons.Length; i++)
        {
            int index = i; // ループ変数をキャプチャするためのローカル変数
            if (buttons[i] != null)
            {
                buttons[i].onClick.AddListener(() =>
                {
                    if (isTextLanguageChanged)
                    {
                        OnTextLanguageChanged?.Invoke((LanguageEnum)index);
                    }
                    else
                    {
                        OnVoiceLanguageChanged?.Invoke((LanguageEnum)index);
                    }
                });
            }
        }
    }
    
    private void UnregisterButtonArray(Button[] buttons)
    {
        if (buttons == null) return;
        
        foreach (var button in buttons)
        {
            if (button != null)
                button.onClick.RemoveAllListeners();
        }
    }
    
    private void OnCloseButtonClick() => OnCloseButtonClicked?.Invoke();
    
    private void ScenarioSpeedChanged(int value) => OnScenarioSpeedChanged?.Invoke(value);
    private void ScenarioAutoChanged(bool value) => OnUseAutoChanged?.Invoke(value);

    private void OnDestroy()
    {
        if(_closeButton != null) _closeButton.onClick?.RemoveAllListeners();
        if(_scenarioSpeedLeft != null) _scenarioSpeedLeft.onClick?.RemoveAllListeners();
        if(_scenarioSpeedRight != null) _scenarioSpeedRight.onClick?.RemoveAllListeners();
        if(_useAute != null) _useAute.onClick?.RemoveAllListeners();
        if(_doNotUseAuto != null) _doNotUseAuto.onClick?.RemoveAllListeners();
        
        UnregisterButtonArray(_textLanguage);
        UnregisterButtonArray(_voiceLanguage);
    }
}
