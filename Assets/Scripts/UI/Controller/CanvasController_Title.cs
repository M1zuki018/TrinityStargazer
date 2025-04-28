using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// タイトル画面のキャンバスコントローラー
/// </summary>
public class CanvasController_Title : WindowBase
{
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _creditButton;
    [SerializeField] private Button _quitButton;
    private float _fadeDuration = 1f;
    
    public event Action OnHomeButtonClicked;
    public event Action OnCreditButtonClicked;
    
    public  override UniTask OnAwake()
    {
        if(_startButton != null) _startButton.onClick.AddListener(GameStart);
        if(_creditButton != null) _creditButton.onClick.AddListener(OpenCredit);
        if(_quitButton != null) _quitButton.onClick.AddListener(Quit);
        return base.OnAwake();
    }

    /// <summary>
    /// ゲーム開始→ホーム画面に遷移するボタン
    /// </summary>
    private async void GameStart()
    {
        GlobalFadePanel.RequestFadeOut(_fadeDuration);
        
        await UniTask.Delay(TimeSpan.FromSeconds(_fadeDuration + 1)); // フェードアウト+少し待つ
        
        OnHomeButtonClicked?.Invoke();
    }
    
    /// <summary>
    /// クレジットパネルを開くボタン
    /// </summary>
    private void OpenCredit() => OnCreditButtonClicked?.Invoke();

    /// <summary>
    /// ゲーム終了処理
    /// </summary>
    private void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
    }

    private void OnDestroy()
    {
        if(_startButton != null) _startButton.onClick?.RemoveAllListeners();
        if(_creditButton != null) _creditButton.onClick?.RemoveAllListeners();
        if(_quitButton != null) _quitButton.onClick?.RemoveAllListeners();
    }

    public override void Show()
    {
        GlobalFadePanel.RequestFadeIn(2.0f);
        base.Show();
    }
}
