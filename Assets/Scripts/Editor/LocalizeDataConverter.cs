using System.IO;
using UnityEditor;
using UnityEngine;

/// <summary>
/// エディター拡張で設定したローカライズデータをランタイム用に変換する
/// </summary>
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