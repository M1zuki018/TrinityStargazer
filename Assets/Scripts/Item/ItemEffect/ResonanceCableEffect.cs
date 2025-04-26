using System.Collections.Generic;

/// <summary>
/// 共鳴ケーブルの効果データ
/// </summary>
public class ResonanceCableEffect : IItemEffect
{
    private int _limitCount;
    private int _remainingTurns;

    public ResonanceCableEffect(int limitCount, int remainingTurns)
    {
        _limitCount = limitCount;
        _remainingTurns = remainingTurns;
    }
    
    public void Apply(IBattleMediator mediator)
    {
        var directions = new List<DirectionEnum>(_limitCount);
        for (int i = 0; i < _limitCount; i++)
        {
            directions.Add((DirectionEnum)i); // TODO: ランダム性を持たせる
        }
        mediator.BattleJudge.LinkingDirection(directions); // リンクさせる処理を呼ぶ
    }

    public void Remove(IBattleMediator mediator)
    {
        mediator.BattleJudge.ReleasingDirection();
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
