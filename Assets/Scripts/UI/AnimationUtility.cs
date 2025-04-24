using DG.Tweening;
using UnityEngine;

/// <summary>
/// DOTweenアニメーションを補助する静的クラス
/// </summary>
public static class AnimationUtility
{
    /// <summary>
    /// 拡大と縮小を繰り返すアニメーション
    /// </summary>
    public static Tween Expand(Transform transform, float duration = 1.0f, float mag = 1.05f)
    {
        return transform.DOScale(mag, duration).SetLoops(-1, LoopType.Yoyo);
    }

    /// <summary>
    /// フェード処理
    /// </summary>
    public static void Fade(CanvasGroup canvasGroup, float duration = 1.0f, float alpha = 1.0f)
    {
        canvasGroup.DOFade(alpha, duration);
    }
}