using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// タイトル画面のキャンバスコントローラー
/// </summary>
public class CanvasController_Title : WindowBase
{
    [SerializeField] private Button _startButton;
    public event Action OnHomeButtonClicked;
    
    public  override UniTask OnAwake()
    {
        if(_startButton != null) _startButton.onClick.AddListener(GameStart);
        return base.OnAwake();
    }

    public override UniTask OnUIInitialize()
    {
        AnimationUtility.Expand(_startButton.transform, 2f);
        return base.OnUIInitialize();
    }
    
    /// <summary>
    /// ゲーム開始→ホーム画面に遷移するボタン
    /// </summary>
    private void GameStart() => OnHomeButtonClicked?.Invoke();

    private void OnDestroy()
    {
        if(_startButton != null) _startButton.onClick?.RemoveAllListeners();
    }

    public override void Show()
    {
        GlobalFadePanel.RequestFadeIn(2.0f);
        base.Show();
    }
}
