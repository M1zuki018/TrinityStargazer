using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// バトルに関するシステムのUIとロジック側を繋ぐクラス
/// </summary>
public class BattleSystemPresenter : ViewBase
{
    [Header("CanvasControllerクラス")]
    [SerializeField] private CanvasController_Before _ccBefore;
    [SerializeField] private CanvasController_Direction _ccDirection;
    [SerializeField] private CanvasController_After _ccAfter;
    [SerializeField] private CanvasController_ItemSelect _ccItemSelect;
    
    [Header("その他のUI")]
    [SerializeField] private DirectionalImages _seiImage;
    [SerializeField] private DirectionalImages _playerHandImage;

    private BattleSystemManager _battleSystemManager;
    private TurnManager _turnManager;
    private IBattleMediator _battleMediator;
    
    private IDirectionDecider _directionDecider;
    private IBattleJudge _battleJudge;
    private IVisualUpdater _visualUpdater;

    private DirectionEnum _enemyDirection;
    
    public event Action OnBattleEnded;
    
    public override UniTask OnAwake()
    {
        _directionDecider = new DirectionDecider();
        _battleJudge = new BattleJudge();
        _visualUpdater = new VisualUpdater(_seiImage, _playerHandImage, _ccDirection.DirectionButtons);

        _battleSystemManager = new BattleSystemManager(_directionDecider, _battleJudge, _visualUpdater, this);
        _turnManager = new TurnManager();
        return base.OnAwake();
    }
    
    public override UniTask OnBind()
    {
        _ccBefore.OnBattleButtonClicked += HandleDirection;
        _ccDirection.OnDirectionButtonClicked += HandleVictoryOrDefeat;
        _ccAfter.OnNextButtonClicked += HandleNextTurn;
        _ccItemSelect.OnTestItemClicked += UseItem;
        _turnManager.OnGameFinished += TurnManagerOnOnGameFinished;
        _battleSystemManager.OnVictoryCountChanged += HandleVictory;

        // 封印のページ
        _directionDecider.OnLimitedDirection += _visualUpdater.SetButtonsInteractive;
        _directionDecider.OnUnlimitedDirection += _visualUpdater.SetButtonsNonInteractive;
        
        // 共鳴ケーブル
        _battleJudge.OnLink += _visualUpdater.ChangeButtonColor;
        _battleJudge.OnRelease += _visualUpdater.ResetButtonColor;
        
        return base.OnBind();
    }

    public override UniTask OnStart()
    {
        InitializeUI();
        return base.OnStart();
    }

    /// <summary>
    /// アイテムを使用する
    /// </summary>
    private void UseItem(ItemTypeEnum itemType, RarityEnum rarity, int count)
    {
        _battleMediator = _battleSystemManager.Mediator;
        InventoryManager.Instance.UseItem(_battleMediator, itemType, rarity, count);
    }
    
    /// <summary>
    /// 必要なUIの初期化処理
    /// </summary>
    private void InitializeUI()
    {
        _ccBefore.SetTurnText(_turnManager.TurnText());
    }

    /// <summary>
    /// ゲーム終了処理を呼び出す
    /// </summary>
    private void TurnManagerOnOnGameFinished()
    {
        OnBattleEnded?.Invoke();   
    }

    /// <summary>
    /// 勝利判定と最大ターン数の比較を行う
    /// </summary>
    private void HandleVictory(int victoryCount)
    {
        // 勝利数が最大ターン数を上回ったら
        if (victoryCount >= GameManagerServiceLocator.Instance.GetGameModeData().MaxTurn)
        {
            _turnManager.GameFinished(); // ゲーム終了処理を呼ぶ
        }
    }

    /// <summary>
    /// バトルを始めた時の情報で敵の方向は決めてしまう
    /// </summary>
    private void HandleDirection()
    {
        _enemyDirection = _battleSystemManager.EnemyDirection();
        Debug.Log($"次の方向：{_enemyDirection}");
    }
    
    /// <summary>
    /// 勝敗を管理するメソッド
    /// </summary>
    private void HandleVictoryOrDefeat(DirectionEnum direction)
    {
        _battleSystemManager.ExecuteBattle(direction, _enemyDirection);
        _ccAfter.SetText(_battleSystemManager.IsVictory);
        _ccBefore.SetResultMark(_turnManager.CurrentTurn - 1, _battleSystemManager.IsVictory);
    }

    /// <summary>
    /// アイテム：スマートフォンを使った時に自動でバトルを進行するための処理
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
        
        _ccDirection.OnDirectionButtonClick(direction); // 方向ボタンを押したときのイベントを発火
    }

    /// <summary>
    /// アイテム：逆行のほうきを使ったときに1ターン巻き戻すための処理
    /// （現状アイテム効果のリセット・経過ターン数のリセットは行っていない）
    /// </summary>
    public void UseReverseBroom()
    {
        _turnManager.BackTurn();
        _ccBefore.SetTurnText(_turnManager.TurnText());
        _battleSystemManager.BackTurn();
        _ccBefore.ResetResultMark(_turnManager.CurrentTurn - 1); // CurrentTurnは1オリジンなので、indexとして扱うために-1する
    }

    /// <summary>
    /// 次のターンに移行するときに必要な処理
    /// </summary>
    private void HandleNextTurn()
    {
        _turnManager.NextTurn();
        _battleSystemManager.ResetBattle();
        _ccBefore.SetTurnText(_turnManager.TurnText());
        if(_battleMediator != null)_battleMediator.UpdateEffects();
    }

    /// <summary>
    /// アイテム：決闘の薔薇用　勝利時に獲得するポイントの数を変更する
    /// </summary>
    public void SetGetWinPoint(int getWinPoint)
    {
        _battleSystemManager.SetGetWinPoint(getWinPoint);
    }

    private void OnDestroy()
    {
        _ccBefore.OnBattleButtonClicked -= HandleDirection;
        _ccDirection.OnDirectionButtonClicked -= HandleVictoryOrDefeat;
        _ccAfter.OnNextButtonClicked -= HandleNextTurn;
        _ccItemSelect.OnTestItemClicked -= UseItem;
        _turnManager.OnGameFinished -= TurnManagerOnOnGameFinished;
        _battleSystemManager.OnVictoryCountChanged -= HandleVictory;
        
        // 封印のページ
        _directionDecider.OnLimitedDirection -= _visualUpdater.SetButtonsInteractive;
        _directionDecider.OnUnlimitedDirection -= _visualUpdater.SetButtonsNonInteractive;
        
        // 共鳴ケーブル
        _battleJudge.OnLink -= _visualUpdater.ChangeButtonColor;
        _battleJudge.OnRelease -= _visualUpdater.ResetButtonColor;
    }
}