using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ローカライズデータを一括管理するエディタウィンドウ(AI使用)
/// </summary>
public class LocalizeManagerWindow : EditorWindow
{
    private Vector2 _scrollPosition;
    private List<LocalizeGroup> _groups = new List<LocalizeGroup>();
    private bool _autoSave = true;
    private string _newGroupName = "New Group";
    
    [MenuItem("Tools/Localization/Localize Manager")]
    public static void ShowWindow()
    {
        var window = GetWindow<LocalizeManagerWindow>("Localize Manager");
        window.minSize = new Vector2(800, 600);
        window.Show();
    }
    
    private void OnEnable()
    {
        LoadData();
    }
    
    private void OnGUI()
    {
        DrawToolbar();
        DrawGroups();
        
        if (_autoSave && GUI.changed)
        {
            SaveData();
        }
    }
    
    private void DrawToolbar()
    {
        EditorGUILayout.BeginHorizontal(EditorStyles.toolbar);
        
        if (GUILayout.Button("Scan Scene", EditorStyles.toolbarButton, GUILayout.Width(80)))
        {
            ScanCurrentScene();
        }
        
        GUILayout.Space(10);
        
        EditorGUILayout.LabelField("New Group:", GUILayout.Width(70));
        _newGroupName = EditorGUILayout.TextField(_newGroupName, EditorStyles.toolbarTextField, GUILayout.Width(120));
        
        if (GUILayout.Button("Add Group", EditorStyles.toolbarButton, GUILayout.Width(80)))
        {
            AddNewGroup(_newGroupName);
            _newGroupName = "New Group";
        }
        
        GUILayout.FlexibleSpace();
        
        _autoSave = GUILayout.Toggle(_autoSave, "Auto Save", EditorStyles.toolbarButton);
        
        if (GUILayout.Button("Save", EditorStyles.toolbarButton, GUILayout.Width(60)))
        {
            SaveData();
        }
        
        if (GUILayout.Button("Load", EditorStyles.toolbarButton, GUILayout.Width(60)))
        {
            LoadData();
        }
        
        EditorGUILayout.EndHorizontal();
    }
    
    private void DrawGroups()
    {
        _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);
        
        for (int i = 0; i < _groups.Count; i++)
        {
            DrawGroup(_groups[i], i);
            GUILayout.Space(10);
        }
        
        EditorGUILayout.EndScrollView();
    }
    
    private void DrawGroup(LocalizeGroup group, int groupIndex)
    {
        // グループヘッダー
        EditorGUILayout.BeginVertical("box");
        
        EditorGUILayout.BeginHorizontal();
        group.isExpanded = EditorGUILayout.Foldout(group.isExpanded, "", true);
        group.groupName = EditorGUILayout.TextField(group.groupName, EditorStyles.boldLabel);
        
        GUILayout.FlexibleSpace();
        
        if (GUILayout.Button("Add Item", GUILayout.Width(80)))
        {
            group.items.Add(new LocalizeItem());
        }
        
        if (GUILayout.Button("X", GUILayout.Width(25)))
        {
            if (EditorUtility.DisplayDialog("Delete Group", 
                $"Are you sure you want to delete '{group.groupName}'?", "Yes", "No"))
            {
                _groups.RemoveAt(groupIndex);
                return;
            }
        }
        
        EditorGUILayout.EndHorizontal();
        
        if (group.isExpanded)
        {
            // カラムヘッダー
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Text Component", EditorStyles.boldLabel, GUILayout.Width(200));
            EditorGUILayout.LabelField("Japanese", EditorStyles.boldLabel, GUILayout.Width(250));
            EditorGUILayout.LabelField("English", EditorStyles.boldLabel, GUILayout.Width(250));
            EditorGUILayout.LabelField("", GUILayout.Width(50)); // Delete button space
            EditorGUILayout.EndHorizontal();
            
            // 区切り線
            var rect = GUILayoutUtility.GetRect(1, 1, GUILayout.ExpandWidth(true));
            EditorGUI.DrawRect(rect, Color.gray);
            
            // アイテム描画
            for (int j = 0; j < group.items.Count; j++)
            {
                DrawLocalizeItem(group.items[j], groupIndex, j);
            }
            
            GUILayout.Space(5);
        }
        
        EditorGUILayout.EndVertical();
    }
    
    private void DrawLocalizeItem(LocalizeItem item, int groupIndex, int itemIndex)
    {
        EditorGUILayout.BeginHorizontal();
        
        // Text Component
        var newTextComponent = (Text)EditorGUILayout.ObjectField(
            item.textComponent, typeof(Text), true, GUILayout.Width(200));
        
        if (newTextComponent != item.textComponent)
        {
            item.textComponent = newTextComponent;
            // Textコンポーネントが設定されたら、既存のテキストを日本語欄に自動設定
            if (newTextComponent != null && string.IsNullOrEmpty(item.japaneseText))
            {
                item.japaneseText = newTextComponent.text;
            }
        }
        
        // Japanese Text
        item.japaneseText = EditorGUILayout.TextArea(item.japaneseText, GUILayout.Width(250), GUILayout.Height(20));
        
        // English Text
        item.englishText = EditorGUILayout.TextArea(item.englishText, GUILayout.Width(250), GUILayout.Height(20));
        
        // Apply button
        if (GUILayout.Button("Apply", GUILayout.Width(50)))
        {
            ApplyLocalization(item);
        }
        
        // Delete button
        if (GUILayout.Button("X", GUILayout.Width(25)))
        {
            _groups[groupIndex].items.RemoveAt(itemIndex);
        }
        
        EditorGUILayout.EndHorizontal();
    }
    
    private void ScanCurrentScene()
    {
        var allTexts = FindObjectsOfType<Text>();
        var ungroupedItems = new List<LocalizeItem>();
        
        foreach (var text in allTexts)
        {
            // 既に登録されているかチェック
            bool alreadyExists = _groups.Any(group => 
                group.items.Any(item => item.textComponent == text));
            
            if (!alreadyExists)
            {
                ungroupedItems.Add(new LocalizeItem
                {
                    textComponent = text,
                    japaneseText = text.text,
                    englishText = ""
                });
            }
        }
        
        if (ungroupedItems.Count > 0)
        {
            var scannedGroup = _groups.FirstOrDefault(g => g.groupName == "Scanned Items");
            if (scannedGroup == null)
            {
                scannedGroup = new LocalizeGroup { groupName = "Scanned Items", isExpanded = true };
                _groups.Add(scannedGroup);
            }
            
            scannedGroup.items.AddRange(ungroupedItems);
        }
        
        Debug.Log($"Scanned {ungroupedItems.Count} new text components.");
    }
    
    private void AddNewGroup(string groupName)
    {
        _groups.Add(new LocalizeGroup 
        { 
            groupName = groupName, 
            isExpanded = true,
            items = new List<LocalizeItem>()
        });
    }
    
    private void ApplyLocalization(LocalizeItem item)
    {
        if (item.textComponent != null)
        {
            // 現在の言語設定に応じてテキストを適用
            var currentLanguage = GetCurrentLanguage();
            item.textComponent.text = currentLanguage == LanguageEnum.Japanese 
                ? item.japaneseText 
                : item.englishText;
                
            EditorUtility.SetDirty(item.textComponent);
        }
    }
    
    private LanguageEnum GetCurrentLanguage()
    {
        // GameManagerServiceLocatorから現在の言語を取得
        if (GameManagerServiceLocator.Instance != null)
        {
            return GameManagerServiceLocator.Instance.Settings.TextLanguage;
        }
        return LanguageEnum.Japanese; // デフォルト
    }
    
    private void SaveData()
    {
        var data = new LocalizeManagerData { groups = _groups };
        var json = JsonUtility.ToJson(data, true);
        File.WriteAllText("Assets/LocalizeManagerData.json", json);
        AssetDatabase.Refresh();
    }
    
    private void LoadData()
    {
        string path = "Assets/LocalizeManagerData.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            var data = JsonUtility.FromJson<LocalizeManagerData>(json);
            _groups = data.groups ?? new List<LocalizeGroup>();
        }
    }
}

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