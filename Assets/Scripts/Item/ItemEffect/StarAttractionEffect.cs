using UnityEngine;

public class StarAttractionEffect : IItemEffect
{
    private int _value;
    private int _remainingTurns;
    
    public StarAttractionEffect(int value, int remainingTurns)
    {
        _value = value;
        _remainingTurns = remainingTurns;
    }
    
    public void Apply(IBattleMediator mediator)
    {
        throw new System.NotImplementedException();
    }

    public void Remove(IBattleMediator mediator)
    {
        throw new System.NotImplementedException();
    }

    public bool IsExpired()
    {
        throw new System.NotImplementedException();
    }

    public void UpdateTurn()
    {
        throw new System.NotImplementedException();
    }
}
