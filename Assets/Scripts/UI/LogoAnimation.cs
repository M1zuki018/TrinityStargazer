using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine.UI;

/// <summary>
/// タイトル画面のロゴ用のアニメーション
/// </summary>
public class LogoAnimation :ViewBase
{
    private Image _image;
    
    public override UniTask OnStart()
    {
        _image = GetComponent<Image>();
        
        // 文字の浮遊アニメーション
        transform.DOMoveY(transform.position.y + 0.2f, 2f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    
        // 文字の輝きアニメーション
        _image.DOFade(0.7f, 1.5f)
            .SetEase(Ease.InOutQuad)
            .SetLoops(-1, LoopType.Yoyo);
        return base.OnStart();
    }
}
