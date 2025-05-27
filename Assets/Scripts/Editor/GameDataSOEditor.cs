using UnityEditor;
using UnityEngine;

/// <summary>
/// GameDataSO用のカスタムエディタ
/// </summary>
[CustomEditor(typeof(GameDataSO))]
public class GameDataSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        EditorGUILayout.Space();
        
        var gameData = (GameDataSO)target;
        
        EditorGUILayout.LabelField("Version Info", EditorStyles.boldLabel);
        EditorGUILayout.LabelField("Full Version:", gameData.GetFullVersionString());
        
        EditorGUILayout.Space();
        
        if (GUILayout.Button("Increment Build Number"))
        {
            gameData.IncrementBuildNumber();
        }
        
        
        if (GUILayout.Button("Sync to PlayerSettings"))
        {
            PlayerSettings.bundleVersion = gameData.Version;
            PlayerSettings.companyName = gameData.AppName;
            Debug.Log("Synced to PlayerSettings");
        }
    }
}