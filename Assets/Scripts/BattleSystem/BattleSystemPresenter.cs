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
    private IVisualUpdater _visualUpdater;
    
    public event Action OnBattleEnded;
    
    public override UniTask OnAwake()
    {
        _directionDecider = new DirectionDecider();
        IBattleJudge battleJudge = new BattleJudge();
        _visualUpdater = new VisualUpdater(_seiImage, _playerHandImage, _ccDirection.DirectionButtons);

        _battleSystemManager = new BattleSystemManager(_directionDecider, battleJudge, _visualUpdater);
        _turnManager = new TurnManager();
        return base.OnAwake();
    }
    
    public override UniTask OnBind()
    {
        _ccDirection.OnDirectionButtonClicked += HandleVictoryOrDefeat;
        _ccAfter.OnNextButtonClicked += HandleNextTurn;
        _ccItemSelect.OnTestItemClicked += UseItem;
        _turnManager.OnGameFinished += TurnManagerOnOnGameFinished;

        _directionDecider.OnLimitedDirection += _visualUpdater.LimitDirectionButton;
        _directionDecider.OnUnlimitedDirection += _visualUpdater.UnlimitDirectionButton;
        
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
        GameManagerServiceLocator.Instance.ItemTest();
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
    /// 勝敗を管理するメソッド
    /// </summary>
    private void HandleVictoryOrDefeat(DirectionEnum direction)
    {
        _battleSystemManager.ExecuteBattle(direction);
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
        _ccDirection.OnDirectionButtonClicked -= HandleVictoryOrDefeat;
        _ccAfter.OnNextButtonClicked -= HandleNextTurn;
        _ccItemSelect.OnTestItemClicked -= UseItem;
        _turnManager.OnGameFinished -= TurnManagerOnOnGameFinished;
        
        _directionDecider.OnLimitedDirection -= _visualUpdater.LimitDirectionButton;
        _directionDecider.OnUnlimitedDirection -= _visualUpdater.UnlimitDirectionButton;
    }
}