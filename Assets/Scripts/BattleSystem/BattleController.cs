using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// バトルシステムの根幹となるクラス
/// </summary>
public class BattleController : IBattleController, IDisposable
{
    public bool IsVictory => _battleState.IsVictory;
    private DirectionEnum _enemyDirection; // 敵が向きを決めるタイミングと勝敗判定のタイミングが異なるため保存しておくための変数
    
    // コンポーネント
    private readonly BattleState _battleState; // バトルの状態管理
    private readonly IDirectionSelector _directionSelector; // 方向決定
    private readonly IBattleEvaluator _battleEvaluator; // 勝敗判定
    private readonly IVisualController _visualController; // バトルに関するUIを管理
    private readonly ITurnCoordinator _turnCoordinator; // ターン管理
    private readonly IItemProcessor _itemProcessor; // アイテムの効果管理
    private readonly IBattleMediator _mediator; // メディエーターパターン

    public event Action OnBattleCompleated;
    public event Action<DirectionEnum> OnDirectionRequest;
    
    public BattleController(DirectionalImages enemyImage, DirectionalImages playerImage, Button[] directionalButtons, TurnUIs turnUIs)
    {
        // 各クラスのインスタンスを生成
        _battleState = new BattleState();
        _directionSelector = new DirectionSelector();
        _battleEvaluator = new BattleEvaluator();
        _visualController = new VisualController(enemyImage, playerImage, directionalButtons, turnUIs);
        _turnCoordinator = new TurnCoordinator();
        _itemProcessor = new ItemProcessor(this);
        _mediator = new BattleMediator(_directionSelector, _battleEvaluator, _visualController, _itemProcessor);
        
        BindBattleComponents(); // イベント購読
        
        _visualController.SetTurnText(_turnCoordinator.GetTurnText()); // ターン表示の初期化
    }

    #region バトルのメインとなる処理
    
    /// <summary>
    /// 敵が向く方向を決定する（方向決定画面を開いたタイミングで呼び出される）
    /// </summary>
    public void DecideEnemyDirection()
    {
        _enemyDirection = _directionSelector.DecideDirection();
        Debug.Log($"次の方向：{_enemyDirection}");
    }
    
    /// <summary>
    /// バトルの実行
    /// </summary>
    public void ExecuteBattle(DirectionEnum playerDirection)
    {
        _visualController.UpdateSprites(_enemyDirection, playerDirection);
        _battleState.ProcessBattleResult(_battleEvaluator.Judge(_enemyDirection, playerDirection));
        
        // 勝利数が最大ターン数を上回ったら
        if (_battleState.IsVictoryConditionMet(BattleManager.Instance.GetGameModeData().MaxTurn))
        {
            _turnCoordinator.CompleteBattle(); // ゲーム終了処理を呼ぶ
        }
        
        _visualController.SetResultMark(GetCurrentTurnIndex(), IsVictory);
    }

    /// <summary>
    /// ターン巻き戻し処理
    /// </summary>
    public void RevertTurn()
    {
        _turnCoordinator.RevertTurn(); // ターン数の巻き戻し
        _visualController.SetTurnText(_turnCoordinator.GetTurnText()); // UIの書き換え
        _visualController.ResetResultMark(GetCurrentTurnIndex());
        _battleState.RevertLastBattleResult(); // 勝利数を変更するか確認して必要なら処理を行う
    }
    
    /// <summary>
    /// 次のターンへ進むときに必要な処理
    /// </summary>
    public void PrepareBattleForNextTurn()
    {
        _turnCoordinator.AdvanceToNextTurn(); // ターン数を進める
        _directionSelector.ResetProbabilities(); // 敵の方向を選ぶ割合をリセットする
        _visualController.ResetSprites(); // 顔の向き、指が指す方向をリセット
        _mediator.UpdateEffects(); // アイテム効果の継続ターンを減らす
        _visualController.SetTurnText(_turnCoordinator.GetTurnText()); // ターンの表示を更新する
    }
    
    #endregion
    
    /// <summary>
    /// アイテムを使用する処理
    /// </summary>
    public void UseItem(ItemTypeEnum itemType, RarityEnum rarity, int count)
    {
        InventoryManager.Instance.UseItem(_mediator, itemType, rarity, count);
    }
    
    /// <summary>
    /// UIインデックスで使うために現在のターン数（1オリジン）-1の数を返す
    /// </summary>
    private int GetCurrentTurnIndex() => _turnCoordinator.CurrentTurn - 1;
    
    /// <summary>
    /// 方向ボタンを押す処理をロジック側から呼び出すためのイベントを発火
    /// </summary>
    public void RequestDirectionSelection(DirectionEnum direction) => OnDirectionRequest?.Invoke(direction);
    
    /// <summary>
    /// 勝利した時に得られるポイント数を変更する
    /// </summary>
    public void SetVictoryPointValue(int pointValue) => _battleState.SetVictoryPointValue(pointValue);

    /// <summary>
    /// Presenterのゲーム終了イベントを発火させる
    /// </summary>
    private void NotifyBattleCompletion()
    {
        BattleManager.Instance.SetVictoryPoints(_battleState.VictoryCount); // 勝敗数を記録する
        OnBattleCompleated?.Invoke();
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
        _directionSelector.OnLimitedDirection += _visualController.SetButtonsInteractive;
        _directionSelector.OnUnlimitedDirection += _visualController.SetButtonsNonInteractive;
        _battleEvaluator.OnLink += _visualController.ChangeButtonColor;
        _battleEvaluator.OnRelease += _visualController.ResetButtonColor;
        _turnCoordinator.OnGameFinished += NotifyBattleCompletion;
    }
    
    /// <summary>
    /// バトルコンポーネント間のイベント接続解除
    /// </summary>
    private void UnbindBattleComponents()
    {
        _directionSelector.OnLimitedDirection -= _visualController.SetButtonsInteractive;
        _directionSelector.OnUnlimitedDirection -= _visualController.SetButtonsNonInteractive;
        _battleEvaluator.OnLink -= _visualController.ChangeButtonColor;
        _battleEvaluator.OnRelease -= _visualController.ResetButtonColor;
        _turnCoordinator.OnGameFinished -= NotifyBattleCompletion;
    }

    #endregion
}