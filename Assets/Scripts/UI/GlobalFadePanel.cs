using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// フェードパネルを管理するクラス
/// </summary>
public class GlobalFadePanel : ViewBase
{
    [SerializeField] private Image _fadePanel;
    private static CanvasGroup _canvasGroup;
    private Tween _currentFadeTween;
    
    public static event Action<float> OnFadeOutRequested; // フェードアウト
    public static event Action<float> OnFadeInRequested; // フェードイン
    public static event Action<Color> OnColorChangeRequested; // 色変更

    public override UniTask OnAwake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        
        OnFadeOutRequested += HandleFadeOutRequest;
        OnFadeInRequested += HandleFadeInRequest;
        OnColorChangeRequested += ChangeColor;
        
        return base.OnAwake();
    }

    /// <summary>
    /// フェードアウトリクエストを要求
    /// </summary>
    public static void RequestFadeOut(float duration = 1.0f) => OnFadeOutRequested?.Invoke(duration);
    
    /// <summary>
    /// フェードインリクエストを要求
    /// </summary>
    public static void RequestFadeIn(float duration = 1.0f) => OnFadeInRequested?.Invoke(duration);
    
    /// <summary>
    /// パネルの色変更を要求
    /// </summary>
    public static void RequestChangeColor(Color color) => OnColorChangeRequested?.Invoke(color);
    
    /// <summary>
    /// フェードアウトリクエストを処理
    /// </summary>
    private async void HandleFadeOutRequest(float duration)
    {
        await FadeOut(duration);
    }
    
    /// <summary>
    /// フェードインリクエストを処理
    /// </summary>
    private async void HandleFadeInRequest(float duration)
    {
        await FadeIn(duration);
    }
    
    /// <summary>
    /// フェードアウト
    /// </summary>
    private async UniTask FadeOut(float duration)
    {
        // 現在のTweenをキャンセル
        if (_currentFadeTween != null && _currentFadeTween.IsActive())
        {
            _currentFadeTween.Kill();
        }
        
        // 初期状態設定
        _canvasGroup.alpha = 0;
        _canvasGroup.blocksRaycasts = true;
        
        _currentFadeTween = _canvasGroup.DOFade(1.0f, duration);
        
        await _currentFadeTween.AsyncWaitForCompletion();
    }

    /// <summary>
    /// フェードイン
    /// </summary>
    private async UniTask FadeIn(float duration)
    {
        // 現在のTweenをキャンセル
        if (_currentFadeTween != null && _currentFadeTween.IsActive())
        {
            _currentFadeTween.Kill();
        }
        
        // 初期状態設定
        _canvasGroup.alpha = 1;
        _canvasGroup.blocksRaycasts = true;
        
        _currentFadeTween = _canvasGroup.DOFade(0f, duration);
        
        await _currentFadeTween.AsyncWaitForCompletion();
        
        // レイキャストのブロックを解除
        _canvasGroup.blocksRaycasts = false;
    }
    
    /// <summary>
    /// フェードの進行状況を直接操作するメソッド
    /// </summary>
    public static void RequestFadeProgress(float alpha)
    {
        _canvasGroup.alpha = alpha;
    }

    /// <summary>
    /// パネルの色変更
    /// </summary>
    private void ChangeColor(Color color)
    {
        _fadePanel.color = color;
    }

    private void OnDestroy()
    {
        OnFadeOutRequested -= HandleFadeOutRequest;
        OnFadeInRequested -= HandleFadeInRequest;
        OnColorChangeRequested -= ChangeColor;
    }
    
}
