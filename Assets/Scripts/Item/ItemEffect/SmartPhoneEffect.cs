using UnityEngine;

/// <summary>
/// スマートフォンの効果データ
/// </summary>
public class SmartPhoneEffect : IItemEffect
{
    private int _accuracyRate;

    public SmartPhoneEffect(int accuracyRate)
    {
        _accuracyRate = accuracyRate;
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
