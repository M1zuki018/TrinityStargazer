using UnityEngine;

/// <summary>
/// 決闘の薔薇の効果データ
/// </summary>
public class ChallengeRoseEffect : IItemEffect
{
    private int _getWinPoint;
    
    public ChallengeRoseEffect(int getWinPoint)
    {
        _getWinPoint = getWinPoint;
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
