using System;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// 星の予測盤の効果データ
/// </summary>
public class CelestialForecastEffect : IItemEffect
{
    private int _remainingTurns = 1; // 効果持続時間 = 1ターン
    private int _accuracyRate;
    private IBattleMediator _mediator;
    private DirectionEnum _effectDirection;
    
    public CelestialForecastEffect(int accuracyRate)
    {
        _accuracyRate = accuracyRate;
    }
    
    public void Apply(IBattleMediator mediator)
    {
        _mediator = mediator;
        _mediator.DirectionDecider.OnEnemyDirectionChanged += HandleDirectionChanged; // 方向が変わった時にUIを切り替えるメソッドを呼び出す
    }

    private void HandleDirectionChanged(DirectionEnum direction)
    {
        if (IsPredictionSuccessful())
        {
            // 乱数が的中率以下の場合は成功判定。正しい予測でUIを更新する
            _effectDirection = direction;
            Debug.Log($"[星の予測盤] 予測成功");
        }
        else
        {
            _effectDirection = GenerateRandomIncorrectDirection();
            Debug.Log($"[星の予測盤] 予測失敗");
        }
        
        _mediator.VisualUpdater.ForecastDirectionButton(_effectDirection);
    }

    /// <summary>
    /// 正しく予測を出すべきか処理を行う
    /// </summary>
    private bool IsPredictionSuccessful()
    {
        return Random.Range(0, 100) <= _accuracyRate;
    }
    
    /// <summary>
    /// 予測失敗時に提示する方向を返す
    /// </summary>
    private DirectionEnum GenerateRandomIncorrectDirection()
    {
        int directionCount = Enum.GetValues(typeof(DirectionEnum)).Length;
        int randomIndex = Random.Range(0, directionCount - 1);
        
        // 正しい方向と同じになった場合は別の方向に変更する
        if (randomIndex >= (int)_effectDirection)
        {
            randomIndex++;
            if (randomIndex >= directionCount) // 範囲を超えた場合は0に戻す
            {
                randomIndex = 0;
            }
        }

        return (DirectionEnum)randomIndex;
    }

    public void Remove(IBattleMediator mediator)
    {
        _mediator.VisualUpdater.ReleaseForecastDirectionButton(_effectDirection); // UIリセット
        _mediator.DirectionDecider.OnEnemyDirectionChanged -= HandleDirectionChanged; // 効果終了タイミングで不要になるため購読解除
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
