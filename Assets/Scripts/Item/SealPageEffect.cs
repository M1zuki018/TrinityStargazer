using System.Collections.Generic;

/// <summary>
/// 封印のページの効果データ
/// </summary>
public class SealPageEffect : IItemEffect
{
    private readonly List<DirectionEnum> _sealedDirections;
    private int _remainingTurns;
    
    public SealPageEffect(List<DirectionEnum> directions, int turns)
    {
        _sealedDirections = directions;
        _remainingTurns = turns;
    }
    
    public void Apply(IBattleMediator mediator)
    {
        foreach (var direction in _sealedDirections)
        {
            mediator.DirectionDecider.LimitProbability(direction);
        }
    }
    
    public void Remove(IBattleMediator mediator)
    {
        foreach (var direction in _sealedDirections)
        {
            mediator.DirectionDecider.RemoveLimitProbability(direction);
        }
        _sealedDirections.Clear();
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