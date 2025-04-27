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
    private readonly BattleSystemPresenter _presenter;
    private readonly IDirectionDecider _directionDecider; // 方向決定
    private readonly IBattleJudge _battleJudge; // 勝敗判定
    private readonly IVisualUpdater _visualUpdater; // UI更新 TODO:Presenterの役目では？
    private readonly TurnHandler _turnHandler; // ターン管理
    private readonly IItemEffecter _itemEffecter; // アイテムの効果管理
    
    private DirectionEnum _enemyDirection;

    public BattleController(DirectionalImages enemyImage, DirectionalImages playerImage, Button[] directionalButtons, BattleSystemPresenter presenter)
    {
        _presenter = presenter;
        _directionDecider = new DirectionDecider();
        _battleJudge = new BattleJudge();
        _visualUpdater = new VisualUpdater(enemyImage, playerImage, directionalButtons);
        _turnHandler = new TurnHandler();
        _itemEffecter = new ItemEffecter(this);
        _mediator = new BattleMediator(_directionDecider, _battleJudge, _visualUpdater, _itemEffecter);
        
        BindBattleComponents(); // イベント購読
    }

    /// <summary>
    /// UIインデックスで使うために現在のターン数（1オリジン）-1の数を返す
    /// </summary>
    public int GetCurrentTurnToIndex() => _turnHandler.CurrentTurn - 1;

    /// <summary>
    /// UI用表示用のターン数表示文字列を返す
    /// </summary>
    public string GetTurnText() => _turnHandler.TurnText();
    
    /// <summary>
    /// 方向ボタンを押す（アイテム：スマートフォン用）
    /// </summary>
    public void PressDirectionButton(DirectionEnum direction)
    {
        _presenter.PressDirectionButton(direction);
    }
    
    /// <summary>
    /// 敵が向く方向を決定する
    /// </summary>
    public void DecideEnemyDirection()
    {
        _enemyDirection = _mediator.DirectionDecider.DecideDirection();
        Debug.Log($"次の方向：{_enemyDirection}");
    }
    
    /// <summary>
    /// バトルの実行
    /// </summary>
    public void ExecuteBattle(DirectionEnum playerDirection)
    {
        _mediator.VisualUpdater.UpdateSprites(_enemyDirection, playerDirection);
        IsVictory = _mediator.BattleJudge.Judge(_enemyDirection, playerDirection);
        if (IsVictory)
        {
            _victoryCount += _getWinPoint;
            _isWinLastBattle = true;
            
            // 勝利数が最大ターン数を上回ったら
            if (_victoryCount >= GameManagerServiceLocator.Instance.GetGameModeData().MaxTurn)
            {
                _turnHandler.GameFinished(); // ゲーム終了処理を呼ぶ
            }
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
        _turnHandler.BackTurn();
        // 前回のバトルで勝っていた場合は勝利数を変更する処理を行う
        if (_isWinLastBattle)
        {
            _victoryCount -= _getWinPoint;
        }
        _presenter.UseReverseBroom();
    }

    /// <summary>
    /// Presenterのゲーム終了イベントを発火させる
    /// </summary>
    private void GameFinished()
    {
        _presenter.GameFinished();
    }
    
    /// <summary>
    /// バトルの初期化
    /// </summary>
    public void ResetBattle()
    {
        _turnHandler.NextTurn();
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
        _turnHandler.OnGameFinished += GameFinished;
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
        _turnHandler.OnGameFinished -= GameFinished;
    }

    #endregion
}