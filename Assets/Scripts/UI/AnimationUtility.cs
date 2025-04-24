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
    public static void Expand(Transform transform, float duration = 1.0f, float mag = 1.05f)
    {
        transform.DOScale(mag, duration).SetLoops(-1, LoopType.Yoyo);
    }
}
