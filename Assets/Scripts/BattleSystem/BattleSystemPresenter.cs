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
        // 自分が方向決定ボタンを押したタイミングで、スプライトの更新を行う
        _ccDirection.OnDirectionButtonClicked += _battleSystemManager.SetSprite;
        _ccDirection.OnDirectionButtonClicked += HandleVictoryOrDefeat;
        
        // 次のターンに進むタイミングで処理を行うもの
        _ccAfter.OnNextButtonClicked += _turnManager.NextTurn; // ターンのカウントを進める
        _ccAfter.OnNextButtonClicked += _battleSystemManager.ResetDirectionProbabilities;
        _ccAfter.OnNextButtonClicked += _seiImage.ResetSprite;
        _ccAfter.OnNextButtonClicked += _playerHandImage.ResetSprite;
        _ccAfter.OnNextButtonClicked += HandleNextTurn;
        
        _turnManager.OnGameFinished += TurnManagerOnOnGameFinished;
        return base.OnBind();
    }

    public override UniTask OnStart()
    {
        _ccBefore.SetTurnText(_turnManager.TurnText()); // ターンテキストの初期化
        return base.OnStart();
    }

    private void TurnManagerOnOnGameFinished() => OnBattleEnded?.Invoke();

    /// <summary>
    /// 勝敗を管理するメソッド
    /// </summary>
    private void HandleVictoryOrDefeat(DirectionEnum direction)
    {
        _ccAfter.SetText(_battleSystemManager.IsVictory);
        _ccBefore.SetResultMark(_turnManager.CurrentTurn - 1, _battleSystemManager.IsVictory);
    }

    /// <summary>
    /// 次のターンに移行するときの処理
    /// </summary>
    private void HandleNextTurn()
    {
        _ccBefore.SetTurnText(_turnManager.TurnText());
    }

    private void OnDestroy()
    {
        _ccDirection.OnDirectionButtonClicked -= _battleSystemManager.SetSprite;
        _ccDirection.OnDirectionButtonClicked -= HandleVictoryOrDefeat;
        
        _ccAfter.OnNextButtonClicked -= _turnManager.NextTurn;
        _ccAfter.OnNextButtonClicked -= _battleSystemManager.ResetDirectionProbabilities;
        _ccAfter.OnNextButtonClicked -= _seiImage.ResetSprite;
        _ccAfter.OnNextButtonClicked -= _playerHandImage.ResetSprite;
        
        _turnManager.OnGameFinished -= TurnManagerOnOnGameFinished;
    }
}
