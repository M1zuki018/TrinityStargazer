/// <summary>
/// 決闘の薔薇の効果データ
/// </summary>
public class ChallengeRoseEffect : IItemEffect
{
    private int _remainingTurns = 1; // 効果持続時間 = 1ターン
    private int _getWinPoint; // 勝利時に獲得するポイント
    
    public ChallengeRoseEffect(int getWinPoint)
    {
        _getWinPoint = getWinPoint;
    }
    
    public void Apply(IBattleMediator mediator)
    {
        mediator.BattleSystemPresenter.SetGetWinPoint(_getWinPoint);
    }

    public void Remove(IBattleMediator mediator)
    {
        mediator.BattleSystemPresenter.SetGetWinPoint(1); // リセット
    }

    public bool IsExpired()
    {
        return _remainingTurns <= 0;
    }

    public void UpdateTurn()
    {
        _remainingTurns--;
    }
}
