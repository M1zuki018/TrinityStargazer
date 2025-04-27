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

    // Controllerレイヤーのクラス
    private BattleController _battleController;
    
    public event Action OnBattleEnded;
    
    public override UniTask OnAwake()
    {
        // Controllerレイヤーのクラスのインスタンスを生成
        _battleController = new BattleController(_seiImage, _playerHandImage, _ccDirection.DirectionButtons, this);
        return base.OnAwake();
    }
    
    public override UniTask OnBind()
    {
        // UIイベント登録
        _ccBefore.OnBattleButtonClicked += _battleController.DecideEnemyDirection;
        _ccDirection.OnDirectionButtonClicked += HandleVictoryOrDefeat;
        _ccAfter.OnNextButtonClicked += HandleNextTurn;
        _ccItemSelect.OnTestItemClicked += UseItem;
        return base.OnBind();
    }
    
    public override UniTask OnStart()
    {
        InitializeUI();
        return base.OnStart();
    }
    
    /// <summary>
    /// 必要なUIの初期化処理
    /// </summary>
    private void InitializeUI()
    {
        _ccBefore.SetTurnText(_battleController.GetTurnText());
    }
    
    /// <summary>
    /// アイテムを使用する
    /// </summary>
    private void UseItem(ItemTypeEnum itemType, RarityEnum rarity, int count)
    {
        InventoryManager.Instance.UseItem(_battleController.Mediator, itemType, rarity, count);
    }
    
    /// <summary>
    /// 方向ボタンを押す（スマートフォン用）
    /// </summary>
    public void PressDirectionButton(DirectionEnum direction)
    {
        _ccDirection.OnDirectionButtonClick(direction);
    }
    
    /// <summary>
    /// ゲーム終了処理を呼び出す
    /// </summary>
    public void GameFinished()
    {
        OnBattleEnded?.Invoke();   
    }
    
    /// <summary>
    /// 勝敗を管理するメソッド
    /// 引数としてUI側からプレイヤーが選択した　方向が渡される
    /// </summary>
    private void HandleVictoryOrDefeat(DirectionEnum direction)
    {
        _battleController.ExecuteBattle(direction);
        _ccAfter.SetText(_battleController.IsVictory);
        _ccBefore.SetResultMark(_battleController.GetCurrentTurnToIndex(), _battleController.IsVictory);
    }
    
    /// <summary>
    /// 次のターンに移行するときに必要な処理
    /// </summary>
    private void HandleNextTurn()
    {
        _battleController.ResetBattle();
        _ccBefore.SetTurnText(_battleController.GetTurnText());
        _battleController.Mediator.UpdateEffects();
    }
    
    /// <summary>
    /// アイテム：逆行のほうきを使ったときに1ターン巻き戻すための処理
    /// （現状アイテム効果のリセット・経過ターン数のリセットは行っていない）
    /// </summary>
    public void UseReverseBroom()
    {
        _battleController.BackTurn();
        _ccBefore.SetTurnText(_battleController.GetTurnText());
        _ccBefore.ResetResultMark(_battleController.GetCurrentTurnToIndex());
    }
    

    private void OnDestroy()
    {
        _ccBefore.OnBattleButtonClicked -= _battleController.DecideEnemyDirection;
        _ccDirection.OnDirectionButtonClicked -= HandleVictoryOrDefeat;
        _ccAfter.OnNextButtonClicked -= HandleNextTurn;
        _ccItemSelect.OnTestItemClicked -= UseItem;
    }
}