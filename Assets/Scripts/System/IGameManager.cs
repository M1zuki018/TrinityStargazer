/// <summary>
/// 依存性を低減させるためのGameManager用のインターフェース
/// </summary>
public interface IGameManager
{
    bool IsFirstLoad { get; }
    int VictoryPoints { get; }
    ModeData GetGameModeData();
    void SetGameMode(GameModeEnum mode);
    void SetGameState(GameStateEnum gameState);
    int SetVictoryPoints(int points);
}
