using System;
using UnityEngine;

/// <summary>
/// プレイヤーのゲーム設定
/// </summary>
public class GameSettings : ScriptableObject
{
    [Header("グラフィック設定")]
    [SerializeField] private float a = 1;
    
    [Header("サウンド設定")]
    [SerializeField, Range(0,1)] private float _masterVolume = 1.0f;
    [SerializeField, Range(0,1)] private float _bgmVolume = 1.0f;
    [SerializeField, Range(0,1)] private float _seVolume = 1.0f;
    [SerializeField, Range(0,1)] private float _ambientVolume = 1.0f;
    [SerializeField, Range(0,1)] private float _voiceVolume = 1.0f;

    [Header("環境設定")] 
    [SerializeField] private LanguageEnum _textLanguage = LanguageEnum.Japanese;
    [SerializeField] private LanguageEnum _voiceLanguage = LanguageEnum.Japanese;
    [SerializeField] private ScenarioSpeedEnum _scenarioSpeed = ScenarioSpeedEnum.Default;
    [SerializeField] private bool _useAuto = true;
    
    private float GetFloat(string key, float value) =>
        PlayerPrefs.GetFloat(key, value);

    private void SetFloat(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
        PlayerPrefs.Save();
    }
    
    private int GetInt(string key, int value) =>
        PlayerPrefs.GetInt(key, value);

    private void SetInt(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }

    private T GetEnum<T>(string key, T value) where T : Enum
    {
        int intValue = PlayerPrefs.GetInt(key, Convert.ToInt32(value));
        return (T)Enum.ToObject(typeof(T), intValue);
    }

    private void SetEnum<T>(string key, T value) where T : Enum
    {
        PlayerPrefs.SetInt(key, Convert.ToInt32(value));
        PlayerPrefs.Save();
    }

    private bool GetBool(string key, bool value) => 
        PlayerPrefs.GetInt(key, Convert.ToInt32(value)) != 0;

    private void SetBool(string key, bool value)
    {
        PlayerPrefs.SetInt(key, Convert.ToInt32(value));
        PlayerPrefs.Save();
    }

    #region サウンド設定

    /// <summary>
    /// 全体の音量
    /// </summary>
    public float MasterVolume
    {
        get => GetFloat("MasterVolume", _masterVolume);
        set => SetFloat("MasterVolume", value);
    }

    /// <summary>
    /// BGMの音量
    /// </summary>
    public float BGMVolume
    {
        get => GetFloat("BGMVolume", _bgmVolume);
        set => SetFloat("BGMVolume", value);
    }

    /// <summary>
    /// SEの音量
    /// </summary>
    public float SEVolume
    {
        get => GetFloat("SEVolume", _seVolume);
        set => SetFloat("SEVolume", value);
    }

    /// <summary>
    /// 環境音の音量
    /// </summary>
    public float AmbientVolume
    {
        get => GetFloat("AmbientVolume", _ambientVolume);
        set => SetFloat("AmbientVolume", value);
    }

    /// <summary>
    /// ボイスの音量
    /// </summary>
    public float VoiceVolume
    {
        get => GetFloat("VoiceVolume", _voiceVolume);
        set => SetFloat("VoiceVolume", value);
    }

    #endregion

    public LanguageEnum TextLanguage
    {
        get => GetEnum("TextLanguage", _textLanguage);
        set => SetEnum("TextLanguage", value);
    }

    public LanguageEnum VoiceLanguage
    {
        get => GetEnum("VoiceLanguage", _voiceLanguage);
        set => SetEnum("VoiceLanguage", value);
    }

    public ScenarioSpeedEnum ScenarioSpeed
    {
        get => GetEnum("ScenarioSpeed", _scenarioSpeed);
        set => SetEnum("ScenarioSpeed", value);
    }

    public bool UseAuto
    {
        get => GetBool("UseAuto", _useAuto);
        set => SetBool("UseAuto", value);
    }
}
