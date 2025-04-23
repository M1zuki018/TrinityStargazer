/// <summary>
/// プレイヤー情報を保持しておくための静的クラス
/// </summary>
public static class PlayerData
{
    public static int Level { get; } = 1;
    public static GameModeEnum GameMode { get; private set; } = GameModeEnum.Normal;
    
    /// <summary>
    /// ゲームモードを変更する
    /// </summary>
    public static void SetGameMode(GameModeEnum mode) => GameMode = mode;
}
