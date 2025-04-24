using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// ボタンアニメーション用のクラス
/// </summary>
public class ButtonAnimator : ViewBase, IPointerEnterHandler, IPointerExitHandler
{
    private Tween _pulseTween;
    private Vector3 _originalScale;
    private float _pulseDuration = 3.0f;
    private float _pulseMagnitude = 1.05f;
    private Text _text;

    public override UniTask OnStart()
    {
        _originalScale = transform.localScale;
        _text = GetComponentInChildren<Text>();
        StartPulseAnimation();
        TextAnimation();
        return base.OnStart();
    }

    private void TextAnimation()
    {
        // ボタンテキストの光るアニメーション
        _text.DOColor(new Color(1f, 1f, 1f, 0.8f), 1.2f)
            .SetEase(Ease.InOutQuad)
            .SetLoops(-1, LoopType.Yoyo);
    }
    
    /// <summary>
    /// アニメーションを開始する処理
    /// </summary>
    private void StartPulseAnimation()
    {
        _pulseTween?.Kill(); // 既存のアニメーションをキル
     
        _pulseTween = transform.DOScale(_originalScale * _pulseMagnitude, _pulseDuration)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutQuad);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _pulseTween?.Kill();
        transform.DOScale(_originalScale * 1.1f, 0.3f).SetEase(Ease.OutBack); // ホバーアニメーション
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.DOScale(_originalScale, 0.3f)
            .SetEase(Ease.OutQuad)
            .OnComplete(StartPulseAnimation);　// 新しくアニメーションを始める
    }

    private void OnDestroy()
    {
        _pulseTween?.Kill();　// 破棄時にTweenをキル
    }
}