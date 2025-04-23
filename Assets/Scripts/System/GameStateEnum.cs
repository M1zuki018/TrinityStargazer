/// <summary>
/// ゲームの状態を表す列挙型
/// </summary>
public enum GameStateEnum
{
    /// <summary>
    /// タイトル
    /// </summary>
    Title,
    
    /// <summary>
    /// メイン画面
    /// </summary>
    Home,
    
    /// <summary>
    /// メニュー画面
    /// </summary>
    Menu,
    
    /// <summary>
    /// ステージ選択
    /// </summary>
    StageSelect,
    
    /// <summary>
    /// ゲーム
    /// </summary>
    InGame,
    
    /// <summary>
    /// ポーズ中
    /// </summary>
    Pause,
    
    /// <summary>
    /// リザルト
    /// </summary>
    Result,
    
    /// <summary>
    /// ショップ
    /// </summary>
    Shop,
}
