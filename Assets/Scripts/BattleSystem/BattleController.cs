using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// バトルシステムの根幹となるクラス
/// </summary>
public class BattleController : IDisposable
{
    private readonly IBattleMediator _mediator; // バトルに必要な機能が入ったインターフェースなどはこのクラスで管理
    public IBattleMediator Mediator => _mediator;
    public bool IsVictory { get; private set; }
    private bool _isWinLastBattle; // 前回のバトルで勝利したか
    private int _victoryCount;
    private int _getWinPoint = 1;
    
    // コンポーネント
    private IDirectionDecider _directionDecider;
    private IBattleJudge _battleJudge;
    private IVisualUpdater _visualUpdater;
    
    public event Action<int> OnVictoryCountChanged;

    public BattleController(DirectionalImages enemyImage, DirectionalImages playerImage, Button[] directionalButtons, BattleSystemPresenter presenter)
    {
        _directionDecider = new DirectionDecider();
        _battleJudge = new BattleJudge();
        _visualUpdater = new VisualUpdater(enemyImage, playerImage, directionalButtons);
        _mediator = new BattleMediator(_directionDecider, _battleJudge, _visualUpdater, presenter);
        
        BindBattleComponents(); // イベント購読
    }
    
    /// <summary>
    /// 敵が向く方向を決定して返す
    /// </summary>
    public DirectionEnum EnemyDirection()
    {
        return _mediator.DirectionDecider.DecideDirection();
    }
    
    /// <summary>
    /// バトルの実行
    /// </summary>
    public void ExecuteBattle(DirectionEnum playerDirection, DirectionEnum enemyDirection)
    {
        _mediator.VisualUpdater.UpdateSprites(enemyDirection, playerDirection);
        IsVictory = _mediator.BattleJudge.Judge(enemyDirection, playerDirection);
        if (IsVictory)
        {
            _victoryCount += _getWinPoint;
            _isWinLastBattle = true;
            OnVictoryCountChanged?.Invoke(_victoryCount);
        }
        else
        {
            _isWinLastBattle = false;
        }
    }

    /// <summary>
    /// 勝利した時に得られるポイント数を変更する
    /// </summary>
    public void SetGetWinPoint(int getWinPoint) => _getWinPoint = getWinPoint;

    /// <summary>
    /// ターン巻き戻し処理
    /// </summary>
    public void BackTurn()
    {
        // 前回のバトルで勝っていた場合は勝利数を変更する処理を行う
        if (_isWinLastBattle)
        {
            _victoryCount -= _getWinPoint;
            OnVictoryCountChanged?.Invoke(_victoryCount);
        }
    }
    
    /// <summary>
    /// バトルの初期化
    /// </summary>
    public void ResetBattle()
    {
        _mediator.DirectionDecider.ResetProbabilities();
        _mediator.VisualUpdater.ResetSprites();
    }
    
    public void Dispose()
    {
        UnbindBattleComponents(); // イベント購読解除
    }

    #region イベント購読/解除のメソッド

    /// <summary>
    /// バトルコンポーネント間のイベント接続
    /// </summary>
    private void BindBattleComponents()
    {
        _directionDecider.OnLimitedDirection += _visualUpdater.SetButtonsInteractive;
        _directionDecider.OnUnlimitedDirection += _visualUpdater.SetButtonsNonInteractive;
        _battleJudge.OnLink += _visualUpdater.ChangeButtonColor;
        _battleJudge.OnRelease += _visualUpdater.ResetButtonColor;
    }
    
    /// <summary>
    /// バトルコンポーネント間のイベント接続解除
    /// </summary>
    private void UnbindBattleComponents()
    {
        _directionDecider.OnLimitedDirection -= _visualUpdater.SetButtonsInteractive;
        _directionDecider.OnUnlimitedDirection -= _visualUpdater.SetButtonsNonInteractive;
        _battleJudge.OnLink -= _visualUpdater.ChangeButtonColor;
        _battleJudge.OnRelease -= _visualUpdater.ResetButtonColor;
    }

    #endregion
}