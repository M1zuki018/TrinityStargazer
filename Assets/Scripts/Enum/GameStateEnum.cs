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
    /// ゲームモード選択
    /// </summary>
    ModeSelect,
    
    /// <summary>
    /// ショップ
    /// </summary>
    Shop,
    
    /// <summary>
    /// メニュー画面
    /// </summary>
    ItemMenu,
    
    /// <summary>
    /// 設定画面
    /// </summary>
    Settings,
    
    /// <summary>
    /// バトルの一番ベースとなる画面状態
    /// </summary>
    Base,
    
    /// <summary>
    /// 方向選択前
    /// </summary>
    Before,
    
    /// <summary>
    /// アイテム選択画面
    /// </summary>
    ItemSelect,
    
    /// <summary>
    /// チャット画面
    /// </summary>
    Chat,
    
    /// <summary>
    /// ポーズ画面
    /// </summary>
    Pause,
    
    /// <summary>
    /// 方向選択
    /// </summary>
    Direction,
    
    /// <summary>
    /// あっちむいてほいの結果
    /// </summary>
    After,
    
    /// <summary>
    /// リザルト画面
    /// </summary>
    Result,
}
