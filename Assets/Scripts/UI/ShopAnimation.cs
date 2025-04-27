using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ショップ画面を開いたときのアニメーション
/// </summary>
public class ShopAnimation : MonoBehaviour
{
    [SerializeField] private Image _panel;
    [SerializeField] private Image _door;
    
    [SerializeField] private CanvasGroup _shopCanvasGroup;

    private RectTransform _rectTransform;
    private Sequence _openingSequence;

    [Header("店内")] 
    [SerializeField] private RectTransform[] _menuButtons = new RectTransform[3];
    [SerializeField] private CanvasGroup _characterCanvasGroup;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public void OnEnable()
    {
        GlobalFadePanel.RequestChangeColor(Color.white); // ホワイトアウト用に色を変えておく

        Sequence openingSequence = DOTween.Sequence();

        openingSequence.Append(_rectTransform.DOScale(0.9f, 1.5f).SetEase(Ease.OutQuad)); // 縮小
        
        // ホワイトアウトと画面に近付くシーケンス
        Sequence zoomAndFadeSequence = DOTween.Sequence();
        zoomAndFadeSequence.Append(_rectTransform.DOScale(1.3f, 0.5f).SetEase(Ease.OutBack));
        zoomAndFadeSequence.Join(DOTween.To(() => 0f, GlobalFadePanel.RequestFadeProgress, 1f, 0.5f).SetEase(Ease.InQuad));
        
        openingSequence.Append(zoomAndFadeSequence);
        
        openingSequence.OnComplete(() =>
        {
            // ホワイトアウト中にUIを準備するシーケンス
            Sequence setupSequence = DOTween.Sequence();

            // ホワイトアウト中にショップUIを非表示に
            setupSequence.AppendInterval(0.8f);
            setupSequence.AppendCallback(() =>
            {
                _shopCanvasGroup.alpha = 0f;
                _shopCanvasGroup.blocksRaycasts = false;

            });

            setupSequence.OnComplete(() =>
            {
                GlobalFadePanel.RequestFadeIn(1.5f); // フェードイン開始

                // ショップ表示シーケンス
                Sequence revealSequence = DOTween.Sequence();

                // メニューボタンを順番に表示
                float buttonDelay = 0.15f;
                for (int i = 0; i < _menuButtons.Length; i++)
                {
                    float delay = 1.5f + (i * buttonDelay); // フェードイン後にボタンを表示開始
                    revealSequence.Insert(delay, _menuButtons[i].DOScale(1.1f, 0.3f).From(0f).SetEase(Ease.OutBack));
                    revealSequence.Insert(delay + 0.2f, _menuButtons[i].DOScale(1f, 0.1f).SetEase(Ease.OutQuad));
                    revealSequence.Insert(0.8f, _characterCanvasGroup.DOFade(1f, 1.2f).SetEase(Ease.OutCubic));
                }
            });

        });
    }
}
