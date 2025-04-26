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

        _battleSystemManager = new BattleSystemManager(_directionDecider, _battleJudge, _visualUpdater);
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

        // 封印のページ
        _directionDecider.OnLimitedDirection += _visualUpdater.LimitDirectionButton;
        _directionDecider.OnUnlimitedDirection += _visualUpdater.UnlimitDirectionButton;
        
        // 共鳴ケーブル
        _battleJudge.OnLink += _visualUpdater.LinkDirectionButton;
        _battleJudge.OnRelease += _visualUpdater.ReleaseDirectionButton;
        
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

    private void TurnManagerOnOnGameFinished() => OnBattleEnded?.Invoke();

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
    /// 次のターンに移行するときに必要な処理
    /// </summary>
    private void HandleNextTurn()
    {
        _turnManager.NextTurn();
        _battleSystemManager.ResetBattle();
        _ccBefore.SetTurnText(_turnManager.TurnText());
        if(_battleMediator != null)_battleMediator.UpdateEffects();
    }

    private void OnDestroy()
    {
        _ccBefore.OnBattleButtonClicked -= HandleDirection;
        _ccDirection.OnDirectionButtonClicked -= HandleVictoryOrDefeat;
        _ccAfter.OnNextButtonClicked -= HandleNextTurn;
        _ccItemSelect.OnTestItemClicked -= UseItem;
        _turnManager.OnGameFinished -= TurnManagerOnOnGameFinished;
        
        // 封印のページ
        _directionDecider.OnLimitedDirection -= _visualUpdater.LimitDirectionButton;
        _directionDecider.OnUnlimitedDirection -= _visualUpdater.UnlimitDirectionButton;
        
        // 共鳴ケーブル
        _battleJudge.OnLink -= _visualUpdater.LinkDirectionButton;
        _battleJudge.OnRelease -= _visualUpdater.ReleaseDirectionButton;
    }
}