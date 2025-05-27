using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ランタイムでローカライズを管理するコンポーネント
/// エディタで設定したデータを自動適用
/// </summary>
public class RuntimeLocalizeManager : ViewBase
{
    private Dictionary<Text, RuntimeLocalizeItem> _textToItemMap;
    [SerializeField] private TextAsset _runtimeData; // インスペクターでJSONファイルを設定

    public override UniTask OnUIInitialize()
    {
        InitializeTextMapping();
        ApplyCurrentLanguage();
        
        // 言語変更イベントに登録
        if (GameManagerServiceLocator.Instance != null)
        {
            GameManagerServiceLocator.Instance.Settings.OnTextLanguageChanged += OnLanguageChanged;
        }
        return base.OnUIInitialize();
    }
    
    private void OnDestroy()
    {
        if (GameManagerServiceLocator.Instance != null)
        {
            GameManagerServiceLocator.Instance.Settings.OnTextLanguageChanged -= OnLanguageChanged;
        }
    }

    private void InitializeTextMapping()
    {
        _textToItemMap = new Dictionary<Text, RuntimeLocalizeItem>();
        
        // TextAssetからJSONデータを読み込み
        if (_runtimeData != null)
        {
            string json = _runtimeData.text;
            var data = JsonUtility.FromJson<RuntimeLocalizeData>(json);
            
            foreach (var group in data.groups)
            {
                foreach (var item in group.items)
                {
                    if (item.textComponent != null)
                    {
                        _textToItemMap[item.textComponent] = item;
                    }
                }
            }
        }
        else
        {
            Debug.LogWarning("Runtime Localize Data が設定されていません。");
        }
    }
    
    private void OnLanguageChanged(LanguageEnum newLanguage)
    {
        ApplyCurrentLanguage();
    }
    
    /// <summary>
    /// 現在の言語を適用する
    /// </summary>
    private void ApplyCurrentLanguage()
    {
        var currentLanguage = GetCurrentLanguage();
        
        foreach (var kvp in _textToItemMap)
        {
            var textComponent = kvp.Key;
            var item = kvp.Value;
            
            if (textComponent != null)
            {
                textComponent.text = currentLanguage == LanguageEnum.Japanese 
                    ? item.japaneseText 
                    : item.englishText;
            }
        }
    }
    
    private LanguageEnum GetCurrentLanguage()
    {
        if (GameManagerServiceLocator.Instance != null)
        {
            return GameManagerServiceLocator.Instance.Settings.TextLanguage;
        }
        return LanguageEnum.Japanese;
    }
}

