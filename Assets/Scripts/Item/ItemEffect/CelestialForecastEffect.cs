using System;
using UnityEngine;

/// <summary>
/// 星の予測盤の効果データ
/// </summary>
public class CelestialForecastEffect : IItemEffect, IDisposable
{
    private int _remainingTurns = 1; // 効果持続時間 = 1ターン
    private int _value;
    private IBattleMediator _mediator;
    
    public CelestialForecastEffect(int value)
    {
        // 次どちらを向くか予測する
        _value = value;
        
    }
    
    public void Apply(IBattleMediator mediator)
    {
        _mediator = mediator;
        _mediator.DirectionDecider.OnEnemyDirectionChanged += UIChanged; // 方向が変わった時にUIを切り替えるメソッドを呼び出す
    }

    private void UIChanged(DirectionEnum direction)
    {
        
    }

    public void Remove(IBattleMediator mediator)
    {
        
    }

    public bool IsExpired()
    {
        return _remainingTurns <= 0;
    }

    public void UpdateTurn()
    {
        _remainingTurns--;
    }

    public void Dispose()
    {
        _mediator.DirectionDecider.OnEnemyDirectionChanged -= UIChanged;
    }
}
