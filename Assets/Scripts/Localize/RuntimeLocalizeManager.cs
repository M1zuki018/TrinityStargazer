using System;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEditor;
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

// ランタイム用のデータクラス（UnityEditorに依存しない）
[Serializable]
public class RuntimeLocalizeData
{
    public List<RuntimeLocalizeGroup> groups = new List<RuntimeLocalizeGroup>();
}

[Serializable]
public class RuntimeLocalizeGroup
{
    public string groupName = "New Group";
    public List<RuntimeLocalizeItem> items = new List<RuntimeLocalizeItem>();
}

[Serializable]
public class RuntimeLocalizeItem
{
    public Text textComponent;
    public string japaneseText = "";
    public string englishText = "";
}

#if UNITY_EDITOR
// エディタ用のデータクラス（元のLocalizeManagerDataと同じ構造）
[Serializable]
public class LocalizeManagerData
{
    public List<LocalizeGroup> groups = new List<LocalizeGroup>();
}

[Serializable]
public class LocalizeGroup
{
    public string groupName = "New Group";
    public bool isExpanded = true;
    public List<LocalizeItem> items = new List<LocalizeItem>();
}

[Serializable]
public class LocalizeItem
{
    public Text textComponent;
    public string japaneseText = "";
    public string englishText = "";
}

public static class LocalizeDataConverter
{
    [MenuItem("Tools/Localization/Export Runtime Data")]
    public static void ExportRuntimeData()
    {
        // エディタデータを読み込み
        string editorPath = "Assets/LocalizeManagerData.json";
        if (File.Exists(editorPath))
        {
            string json = File.ReadAllText(editorPath);
            var editorData = JsonUtility.FromJson<LocalizeManagerData>(json); // 正しいクラスを使用
            
            // ランタイム用に変換
            var runtimeData = new RuntimeLocalizeData();
            foreach (var editorGroup in editorData.groups)
            {
                var runtimeGroup = new RuntimeLocalizeGroup
                {
                    groupName = editorGroup.groupName
                };
                
                foreach (var editorItem in editorGroup.items)
                {
                    runtimeGroup.items.Add(new RuntimeLocalizeItem
                    {
                        textComponent = editorItem.textComponent,
                        japaneseText = editorItem.japaneseText,
                        englishText = editorItem.englishText
                    });
                }
                runtimeData.groups.Add(runtimeGroup);
            }
            
            // Resourcesフォルダに出力（TextAssetとして使用可能）
            string runtimePath = "Assets/Resources/LocalizeRuntimeData.json";
            Directory.CreateDirectory("Assets/Resources");
            File.WriteAllText(runtimePath, JsonUtility.ToJson(runtimeData, true));
            
            AssetDatabase.Refresh();
            Debug.Log("ランタイムデータを出力しました: " + runtimePath);
            
            // 生成されたJSONファイルを選択状態にする
            var asset = AssetDatabase.LoadAssetAtPath<TextAsset>(runtimePath);
            Selection.activeObject = asset;
            EditorGUIUtility.PingObject(asset);
        }
        else
        {
            Debug.LogError("エディタデータが見つかりません: " + editorPath);
        }
    }
}
#endif