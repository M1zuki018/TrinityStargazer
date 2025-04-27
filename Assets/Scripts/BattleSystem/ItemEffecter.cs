using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// バトルシステムとアイテムの効果処理の連携を担当するクラス
/// </summary>
public class ItemEffecter : IItemEffecter
{
    private BattleController _controller;
    private IDirectionDecider _directionDecider;
    private IBattleJudge _judge; 
    private IVisualUpdater _visual;
    
    public event Action<DirectionEnum> OnEnemyDirectionChanged;
    
    public ItemEffecter(BattleController controller, IDirectionDecider directionDecider, IBattleJudge judge, IVisualUpdater visual)
    {
        _controller = controller;
        _directionDecider = directionDecider;
        _judge = judge;
        _visual = visual;
    }
    
    public void SetButtonsInteractive(DirectionEnum direction)
    {
        _visual.SetButtonsInteractive(direction);
    }

    public void SetButtonsNonInteractive(DirectionEnum direction)
    {
        _visual.SetButtonsNonInteractive(direction);
    }

    public void ChangeButtonColor(DirectionEnum direction, Color color)
    {
        _visual.ChangeButtonColor(direction, color);
    }

    public void ResetButtonColor(DirectionEnum direction)
    {
        _visual.ResetButtonColor(direction);
    }

    public void ForecastDirectionButton(DirectionEnum direction)
    {
        _visual.ForecastDirectionButton(direction);
    }

    public void ReleaseForecastDirectionButton(DirectionEnum direction)
    {
        _visual.ReleaseForecastDirectionButton(direction);
    }

    // アイテム：共鳴ケーブル
    
    public void UseResonanceCable(List<DirectionEnum> directions, ResonanceCableEffect effect)
    {
        _judge.LinkingDirection(directions, effect);
    }

    public void ReleasingDirection(ResonanceCableEffect effect)
    {
        _judge.ReleasingDirection(effect);
    }

    // アイテム：スマートフォン

    /// <summary>
    /// 自動でバトルを進行する
    /// </summary>
    public void UseSmartPhone(DirectionEnum direction)
    {
        UseSmartPhoneAsync(direction).Forget();
    }

    private async UniTask UseSmartPhoneAsync(DirectionEnum direction)
    {
        // ここで一瞬待たないと、敵の方向が決まる前にアイテム使用処理が終わってしまい、
        // 意図した挙動にならないため注意
        await UniTask.Delay(TimeSpan.FromSeconds(1)); // TODO: ここで演出
        
        _controller.RequestDirectionSelection(direction); // 方向ボタンを押したときのイベントを発火
    }
    
    // アイテム：逆行のほうき
    
    /// <summary>
    /// 1ターン巻き戻す
    /// </summary>
    public void UseReverseBroom()
    {
        _controller.RevertTurn();
    }
    
    // アイテム：決闘の薔薇
    
    /// <summary>
    /// 勝利時に獲得するポイントの数を変更する
    /// </summary>
    public void SetGetWinPoint(int getWinPoint)
    {
        _controller.SetVictoryPointValue(getWinPoint);
    }

    public HashSet<DirectionEnum> GetSealedDirections()
    {
        return _directionDecider.GetSealedDirections();
    }

    public void ModifyProbability(DirectionEnum direction, float addedProbability)
    {
        _directionDecider.ModifyProbability(direction, addedProbability);
    }

    public void LimitProbability(DirectionEnum direction)
    {
        _directionDecider.LimitProbability(direction);
    }

    public void RemoveLimitProbability(DirectionEnum direction)
    {
        _directionDecider.RemoveLimitProbability(direction);
    }

    public void ResetProbabilities()
    {
        _directionDecider.ResetProbabilities();   
    }
}
