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
    [SerializeField] private TurnUIs _turnUIs;

    // ロジック側
    private IBattleController _battleController;
    
    public event Action OnBattleEnded; // ゲーム終了を通知するイベント
    
    public override UniTask OnAwake()
    {
        //TODO: この部分の依存性の解消について悩み中
        _battleController = new BattleController(_seiImage, _playerHandImage, _ccDirection.DirectionButtons, _turnUIs);
        return base.OnAwake();
    }
    
    public override UniTask OnBind()
    {
        // UIイベント登録
        _ccBefore.OnBattleButtonClicked += _battleController.DecideEnemyDirection;
        _ccItemSelect.OnTestItemClicked += _battleController.UseItem;
        _ccDirection.OnDirectionButtonClicked += HandleVictoryOrDefeat;
        _ccAfter.OnNextButtonClicked += _battleController.ResetBattle;
        
        // ロジックイベント登録
        _battleController.OnGameFinished += GameFinished;
        _battleController.OnDirectionRequest += _ccDirection.OnDirectionButtonClick;
        return base.OnBind();
    }
    
    /// <summary>
    /// 方向決定したあとの処理。引数としてUI側からプレイヤーが選択した方向が渡される
    /// </summary>
    private void HandleVictoryOrDefeat(DirectionEnum direction)
    {
        _battleController.ExecuteBattle(direction); // バトルの実行
        _ccAfter.SetText(_battleController.IsVictory); // UI書き換え
    }
    
    /// <summary>
    /// ゲーム終了処理を呼び出す
    /// Controllerから呼び出されて、InGameUIManagerが受け取り リザルト画面に進む
    /// </summary>
    private void GameFinished()
    {
        OnBattleEnded?.Invoke();   
    }

    private void OnDestroy()
    {
        _ccBefore.OnBattleButtonClicked -= _battleController.DecideEnemyDirection;
        _ccItemSelect.OnTestItemClicked -= _battleController.UseItem;
        _ccDirection.OnDirectionButtonClicked -= HandleVictoryOrDefeat;
        _ccAfter.OnNextButtonClicked -= _battleController.ResetBattle;
        _battleController.OnGameFinished -= GameFinished;
        _battleController.OnDirectionRequest -= _ccDirection.OnDirectionButtonClick;
    }
}