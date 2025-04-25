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
    
    [Header("その他のUI")]
    [SerializeField] private DirectionalImages _seiImage;
    [SerializeField] private DirectionalImages _playerHandImage;

    private BattleSystemManager _battleSystemManager;
    private TurnManager _turnManager;
    private IItemManager _itemManager;
    
    public event Action OnBattleEnded;
    
    public override UniTask OnAwake()
    {
        IDirectionDecider directionDecider = new DirectionDecider();
        IBattleJudge battleJudge = new BattleJudge();
        IVisualUpdater visualUpdater = new VisualUpdater(_seiImage, _playerHandImage);

        _itemManager = new BattleItemSystem();
        _battleSystemManager = new BattleSystemManager(directionDecider, battleJudge, visualUpdater, _itemManager);
        _turnManager = new TurnManager();
        return base.OnAwake();
    }
    
    public override UniTask OnBind()
    {
        _ccDirection.OnDirectionButtonClicked += HandleVictoryOrDefeat;
        _ccAfter.OnNextButtonClicked += HandleNextTurn;
        _turnManager.OnGameFinished += TurnManagerOnOnGameFinished;
        return base.OnBind();
    }

    public override UniTask OnStart()
    {
        InitializeUI();
        return base.OnStart();
    }

    [ContextMenu("アイテムテスト")]
    public bool UseSealPage(RarityEnum rarity)
    {
        // 1. まずInventoryからアイテムを消費できるか確認
        if (InventoryManager.Instance.UseItem(ItemTypeEnum.SealPage, rarity))
        {
            // 2. 消費できたら戦闘効果を適用
            var sealPage = new SealPage(rarity);
            sealPage.Use(_itemManager);
            return true;
        }
        // 在庫不足などの理由で使用できない
        return false;
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
        _itemManager.UpdateTurn();
    }

    private void OnDestroy()
    {
        _ccDirection.OnDirectionButtonClicked -= HandleVictoryOrDefeat;
        _ccAfter.OnNextButtonClicked -= HandleNextTurn;
        _turnManager.OnGameFinished -= TurnManagerOnOnGameFinished;
    }
}