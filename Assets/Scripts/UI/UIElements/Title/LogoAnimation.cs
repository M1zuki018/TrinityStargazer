using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// タイトル画面のロゴ用のアニメーション
/// </summary>
public class LogoAnimation : UIElementBase<Image>
{
    private void Start()
    {
        // 文字の浮遊アニメーション
        transform.DOMoveY(transform.position.y + 0.2f, 2f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    
        // 文字の輝きアニメーション
        _element.DOFade(0.7f, 1.5f)
            .SetEase(Ease.InOutQuad)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
