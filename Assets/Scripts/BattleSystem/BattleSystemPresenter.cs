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
    
    public override UniTask OnAwake()
    {
        _battleSystemManager = new BattleSystemManager(_seiImage, _playerHandImage);
        return base.OnAwake();
    }
    
    public override UniTask OnBind()
    {
        // 自分が方向決定ボタンを押したタイミングで、スプライトの更新を行う
        _ccDirection.OnDirectionButtonClicked += _battleSystemManager.SetSprite;
        _ccAfter.OnNextButtonClicked += _seiImage.ResetSprite;
        _ccAfter.OnNextButtonClicked += _playerHandImage.ResetSprite;
        return base.OnBind();
    }

    private void OnDestroy()
    {
        _ccDirection.OnDirectionButtonClicked -= _battleSystemManager.SetSprite;
        _ccAfter.OnNextButtonClicked -= _seiImage.ResetSprite;
        _ccAfter.OnNextButtonClicked -= _playerHandImage.ResetSprite;
    }
}
