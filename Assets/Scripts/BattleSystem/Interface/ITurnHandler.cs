using System;

/// <summary>
/// ターンを管理するクラスのためのインターフェース
/// </summary>
public interface ITurnHandler
{
    int CurrentTurn { get; }
    event Action OnGameFinished;
    void AdvanceToNextTurn();
    void RevertTurn();
    void CompleteBattle();
    string GetTurnText();
}
