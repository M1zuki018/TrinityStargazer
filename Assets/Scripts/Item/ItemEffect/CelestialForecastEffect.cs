using UnityEngine;

/// <summary>
/// 星の予測盤の効果データ
/// </summary>
public class CelestialForecastEffect : IItemEffect
{
    private int _remainingTurns = 1; // 効果持続時間 = 1ターン
    private int _value;
    
    public CelestialForecastEffect(int value)
    {
        // 次どちらを向くか予測する
        _value = value;
        
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
        return _remainingTurns <= 0;
    }

    public void UpdateTurn()
    {
        _remainingTurns--;
    }
}
