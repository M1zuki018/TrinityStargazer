using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Imageコンポーネントがついたオブジェクトの表示アニメーションを制御するコンポーネント
/// </summary>
public class ImageAnimator : ViewBase
{
    [Header("アニメーションの設定")] 
    [SerializeField] private float _animationDuration = 0.5f;
    [SerializeField] private float _slideDistance = -50;
    
    [Header("使用するアニメーションの選択")]
    [SerializeField] private bool _enableFade = true;
    [SerializeField] private bool _enableSlide = true;
    
    private Image _targetImage;

    public override UniTask OnStart()
    {
        _targetImage = GetComponent<Image>();
        PlayEntranceAnimation();
        return base.OnStart();
    }

    /// <summary>
    /// 設定に基づいて入場アニメーションを再生する
    /// </summary>
    private void PlayEntranceAnimation()
    {
        if (_enableFade)
        {
            _targetImage.color = new Color(1, 1, 1, 0);
            _targetImage.DOFade(1f, _animationDuration);
        }

        if (_enableSlide)
        {
            transform.DOLocalMoveX(transform.localPosition.x + _slideDistance, _animationDuration).SetEase(Ease.OutBack);
        }
    }
}