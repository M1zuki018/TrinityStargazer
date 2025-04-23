using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// バトルに関するシステムのUIとロジック側を繋ぐクラス
/// </summary>
public class BattleSystemPresenter : ViewBase
{
    [Header("CanvasControllerクラス")]
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
        
        // 次のターンに進むタイミングで処理を行うもの
        _ccAfter.OnNextButtonClicked += _turnManager.NextTurn; // ターンのカウントを進める
        _ccAfter.OnNextButtonClicked += _battleSystemManager.ResetDirectionProbabilities;
        _ccAfter.OnNextButtonClicked += _seiImage.ResetSprite;
        _ccAfter.OnNextButtonClicked += _playerHandImage.ResetSprite;
        
        _turnManager.OnGameFinished += TurnManagerOnOnGameFinished;
        return base.OnBind();
    }

    private void TurnManagerOnOnGameFinished() => OnBattleEnded?.Invoke();

    private void OnDestroy()
    {
        _ccDirection.OnDirectionButtonClicked -= _battleSystemManager.SetSprite;
        
        _ccAfter.OnNextButtonClicked -= _battleSystemManager.ResetDirectionProbabilities;
        _ccAfter.OnNextButtonClicked -= _seiImage.ResetSprite;
        _ccAfter.OnNextButtonClicked -= _playerHandImage.ResetSprite;
    }
}
