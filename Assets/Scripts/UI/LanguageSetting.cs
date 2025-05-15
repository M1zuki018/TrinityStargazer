using System;
using UnityEngine;

/// <summary>
/// 言語設定用のクラス
/// </summary>
public class LanguageSetting : IDisposable
{
    private CanvasController_Settings_Environment _cc;
    private IGameManager _gameManager;

    public LanguageSetting(CanvasController_Settings_Environment cc, IGameManager gameManager)
    {
        _gameManager = gameManager;
        _cc = cc;

        if (_cc != null)
        {
            _cc.OnTextLanguageChanged += CcOnOnTextLanguageChanged;
            _cc.OnVoiceLanguageChanged += CcOnOnVoiceLanguageChanged;
        }
        else
        {
            Debug.LogWarning($"[LanguageSetting]{typeof(CanvasController_Settings_Environment)} がnullです");
        }
    }

    /// <summary>
    /// テキストの言語設定を変更する
    /// </summary>
    private void CcOnOnTextLanguageChanged(LanguageEnum type)
    {
        _gameManager.Settings.TextLanguage = type;
        Debug.Log($"OnTextLanguageChanged: {type}");
    }
    
    /// <summary>
    /// ボイスの言語設定を変更する
    /// </summary>
    private void CcOnOnVoiceLanguageChanged(LanguageEnum type)
    {
        _gameManager.Settings.VoiceLanguage = type;
        Debug.Log($"OnVoiceLanguageChanged: {type}");
    }

    public void Dispose()
    {
        _cc.OnTextLanguageChanged -= CcOnOnTextLanguageChanged;
        _cc.OnVoiceLanguageChanged -= CcOnOnVoiceLanguageChanged;
    }
}
