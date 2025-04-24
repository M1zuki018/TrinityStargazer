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
    
    public event Action OnBattleEnded;
    
    public override UniTask OnAwake()
    {
        _battleSystemManager = new BattleSystemManager(_seiImage, _playerHandImage);
        _turnManager = new TurnManager();
        return base.OnAwake();
    }
    
    public override UniTask OnBind()
    {
        _ccDirection.OnDirectionButtonClicked += HandleVictoryOrDefeat; // 方向決定ボタンを押したタイミングで呼ばれる処理
        _ccAfter.OnNextButtonClicked += HandleNextTurn; // 次のターンに進むタイミングで処理を行うもの
        _turnManager.OnGameFinished += TurnManagerOnOnGameFinished;
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
        _ccBefore.SetTurnText(_turnManager.TurnText()); // ターンテキストの初期化
    }

    private void TurnManagerOnOnGameFinished() => OnBattleEnded?.Invoke();

    /// <summary>
    /// 勝敗を管理するメソッド
    /// </summary>
    private void HandleVictoryOrDefeat(DirectionEnum direction)
    {
        _battleSystemManager.SetSprite(direction);
        _ccAfter.SetText(_battleSystemManager.IsVictory);
        _ccBefore.SetResultMark(_turnManager.CurrentTurn - 1, _battleSystemManager.IsVictory);
    }

    /// <summary>
    /// 次のターンに移行するときに必要な処理
    /// </summary>
    private void HandleNextTurn()
    {
        _turnManager.NextTurn(); // ターンのカウントを進める
        _battleSystemManager.ResetDirectionProbabilities();　// 相手がどの方向を向くか、確率をリセットする
        ResetSprite();
        _ccBefore.SetTurnText(_turnManager.TurnText()); // ターンテキストの更新
    }

    /// <summary>
    /// キャラクターのスプライトをデフォルトに戻す
    /// </summary>
    private void ResetSprite()
    {
        _seiImage.ResetSprite();
        _playerHandImage.ResetSprite();
    }

    private void OnDestroy()
    {
        _ccDirection.OnDirectionButtonClicked -= HandleVictoryOrDefeat;
        _ccAfter.OnNextButtonClicked -= HandleNextTurn;
        _turnManager.OnGameFinished -= TurnManagerOnOnGameFinished;
    }
}
