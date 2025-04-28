using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ショップ画面のキャンバスコントローラー
/// </summary>
public class CanvasController_Shop : WindowBase
{
    [SerializeField] private Button _closeButton;
    [SerializeField] private Button _purchaseButton;
    [SerializeField] private Button _saleButton;
    [SerializeField] private Button _synthesisButton;
    [SerializeField] private ShopAnimation _shopAnimation;
    public event Action OnHomeButtonClicked; // ホーム画面に戻る
    public event Action OnPurchaseButtonClicked;
    public event Action OnSaleButtonClicked;
    public event Action OnSynthesisButtonClicked;

    public override UniTask OnUIInitialize()
    {
        if (_closeButton != null) _closeButton.onClick.AddListener(BackHome);
        if(_purchaseButton != null) _purchaseButton.onClick.AddListener(PurchaseButtonClicked);
        if(_saleButton != null) _saleButton.onClick.AddListener(SaleButtonClicked);
        if(_synthesisButton != null) _synthesisButton.onClick.AddListener(SynthesisButtonClicked);
        
        return base.OnUIInitialize();
    }

    public override void Show()
    {
        GlobalFadePanel.RequestChangeColor(Color.white);
        GlobalFadePanel.RequestFadeIn(0.5f);
        base.Show();
        _shopAnimation.gameObject.SetActive(true); // パネルを開くときにアニメーションを流すようにする
    }

    public override void Hide()
    {
        base.Hide();
        _shopAnimation.gameObject.SetActive(false); // 非表示に戻しておく
    }

    /// <summary>
    /// モード選択画面→ホーム画面に遷移する
    /// </summary>
    private void BackHome() => OnHomeButtonClicked?.Invoke();
    
    /// <summary>
    /// アイテム購入パネルを開く
    /// </summary>
    private void PurchaseButtonClicked() => OnPurchaseButtonClicked?.Invoke();
    
    /// <summary>
    /// アイテム売却パネルを開く
    /// </summary>
    private void SaleButtonClicked() => OnSaleButtonClicked?.Invoke();
    
    /// <summary>
    /// アイテム合成パネルを開く
    /// </summary>
    private void SynthesisButtonClicked() => OnSynthesisButtonClicked?.Invoke();

    private void OnDestroy()
    {
        if(_closeButton != null) _closeButton.onClick?.RemoveAllListeners();
        if(_purchaseButton != null) _purchaseButton.onClick?.RemoveAllListeners();
        if(_saleButton != null) _saleButton.onClick?.RemoveAllListeners();
        if(_synthesisButton != null) _synthesisButton.onClick?.RemoveAllListeners();
    }
}
