using UnityEngine;

/// <summary>
/// ゲームデータ
/// </summary>
public static class GameData
{
    private static GameDataSO _instance;
    
    /// <summary>
    /// GameDataSOのインスタンスを取得
    /// </summary>
    public static GameDataSO Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = Resources.Load<GameDataSO>("GameData");
                if (_instance == null)
                {
                    Debug.LogError("リソースフォルダ内にGameDataSOが見つかりませんでした");
                }
            }
            return _instance;
        }
    }
    
    public static string VERSION => Instance?.Version ?? "0.0.0";
    public static string APP_NAME => Instance?.AppName ?? "Game";
    public static string SNS_URL => Instance?.SnsUrl ?? "";
    public static string FULL_VERSION => Instance?.GetFullVersionString() ?? "0.0.0";
    public static bool IS_DEBUG_MODE => Instance?.IsDebugMode ?? false;
    public static int TARGET_FRAME_RATE => Instance?.TargetFrameRate ?? 60;
}
