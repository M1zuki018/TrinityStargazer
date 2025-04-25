/// <summary>
/// 依存性を低減させるためのGameManager用のインターフェース
/// </summary>
public interface IGameManager
{
    bool IsFirstLoad { get; }
    void SetGameMode(GameModeEnum mode);
    
    void SetGameState(GameStateEnum gameState);
    
    ModeData GetGameModeData();
}
