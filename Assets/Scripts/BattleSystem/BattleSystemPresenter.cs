using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// バトルに関するシステムのUIとロジック側を繋ぐクラス
/// </summary>
public class BattleSystemPresenter : ViewBase
{
    [Header("UI側クラス")]
    [SerializeField] private CanvasController_Direction _ccDirection;
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
        return base.OnBind();
    }

    private void OnDestroy()
    {
        _ccDirection.OnDirectionButtonClicked -= _battleSystemManager.SetSprite;
    }
}
