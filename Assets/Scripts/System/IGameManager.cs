/// <summary>
/// 依存性を低減させるためのGameManager用のインターフェース
/// </summary>
public interface IGameManager
{
    GameSettings Settings { get; }
    bool IsFirstLoad { get; }
    void SetGameState(GameStateEnum gameState);
}
