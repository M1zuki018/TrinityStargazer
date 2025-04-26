using System;
using UnityEngine;

/// <summary>
/// 星の予測盤の効果データ
/// </summary>
public class CelestialForecastEffect : IItemEffect
{
    private int _remainingTurns = 1; // 効果持続時間 = 1ターン
    private int _value;
    private IBattleMediator _mediator;
    private DirectionEnum _effectDirection;
    
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
        _effectDirection = direction;
        
        int rand = UnityEngine.Random.Range(0, 100);
        if (rand <= _value)
        {
            // 乱数が的中率以下の場合は成功判定。正しい予測でUIを更新する
            _mediator.VisualUpdater.ForecastDirectionButton(_effectDirection);
            Debug.Log($"[星の予測盤] 予測成功");
        }
        else
        {
            rand = UnityEngine.Random.Range(0, 8); // 8方向のうち適当な乱数を生成
            if (rand == (int)_effectDirection) // 正しい方向が選ばれた時は値を修正
            {
                rand = (rand + 1) % 8;
            }
            _mediator.VisualUpdater.ForecastDirectionButton((DirectionEnum)rand);
            Debug.Log($"[星の予測盤] 予測失敗");
        }
    }

    public void Remove(IBattleMediator mediator)
    {
        _mediator.VisualUpdater.ReleaseForecastDirectionButton(_effectDirection); // UIリセット
        _mediator.DirectionDecider.OnEnemyDirectionChanged -= UIChanged; // 効果終了タイミングで不要になるため購読解除
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
