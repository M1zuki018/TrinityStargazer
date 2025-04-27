using System;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// スマートフォンの効果データ
/// </summary>
public class SmartPhoneEffect : IItemEffect
{
    private int _remainingTurns = 1; // 効果持続時間 = 1ターン
    private int _accuracyRate;
    private IBattleMediator _mediator;
    private DirectionEnum _effectDirection;

    public SmartPhoneEffect(int accuracyRate)
    {
        _accuracyRate = accuracyRate;
    }
    
    public void Apply(IBattleMediator mediator)
    {
        Debug.Log($"[スマートフォン] やあマキくん。");
        _mediator = mediator;
        _mediator.DirectionSelector.OnEnemyDirectionChanged += ExecuteDirectionPredictionAndBattle;
        SetButtonsInteractive();
    }
    
    /// <summary>
    /// アイテム効果の具体的な処理
    /// 敵の方向が決定された際に予測を行い、バトルを自動進行する
    /// </summary>
    private void ExecuteDirectionPredictionAndBattle(DirectionEnum enemyDirection)
    {
        if (IsPredictionSuccessful())
        {
            // 乱数が的中率以下の場合は成功判定。正しい予測でUIを更新する
            _effectDirection = enemyDirection;
            Debug.Log($"[スマートフォン] 予測成功");
        }
        else
        {
            _effectDirection = GenerateRandomIncorrectDirection();
            Debug.Log($"[スマートフォン] 予測失敗");
        }
        
        _mediator.ItemProcessor.UseSmartPhone(_effectDirection); // 勝敗判定まで進める
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
        SetButtonsNonInteractive();
        _mediator.DirectionSelector.OnEnemyDirectionChanged -= ExecuteDirectionPredictionAndBattle;
    }
    
    /// <summary>
    /// ボタンを押せないようにする
    /// </summary>
    private void SetButtonsInteractive()
    {
        for (int i = 0; i < 8; i++)
        {
            int index = i;
            _mediator.VisualController.SetButtonsInteractive((DirectionEnum)index);
        }
    }

    /// <summary>
    /// ボタンの制限を解除する
    /// </summary>
    private void SetButtonsNonInteractive()
    {
        for (int i = 0; i < 8; i++)
        {
            int index = i;
            _mediator.VisualController.SetButtonsNonInteractive((DirectionEnum)index);
        }
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
