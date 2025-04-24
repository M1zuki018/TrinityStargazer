using UnityEngine;
using DG.Tweening;

/// <summary>
/// タイトル画面でマウスの動きに合わせて傾いて見えるような演出
/// </summary>
public class ImageParallax : ViewBase
{
    [Header("レイヤー要素")]
    [SerializeField] private RectTransform _backgroundLayer;  // 背景部分のマスク領域
    [SerializeField] private RectTransform[] _characterLayers = new RectTransform[1]; // 切り抜いたキャラクター
    [SerializeField] private RectTransform _titleLayer;       // タイトルテキスト

    [Header("パララックス設定")]
    [SerializeField] private float _backgroundFactor = 0.02f; // 背景の移動量
    [SerializeField] private float _characterFactorBase = 0.05f; // キャラクターの基本移動量
    [SerializeField] private float _titleFactor = 0.03f;      // タイトルの移動量
    [SerializeField] private float _uiFactor = 0.01f;         // UIの移動量
    [SerializeField] private float _smoothTime = 0.2f;        // 移動のスムーズさ
    
    // 初期位置を保存
    private Vector2[] _characterInitialPos;
    private Vector2 _backgroundInitialPos;
    private Vector2 _titleInitialPos;
    
    // 画面の中心
    private Vector2 _screenCenter;

    private void Start()
    {
        // 初期位置を保存
        _screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        
        // 背景の初期位置
        _backgroundInitialPos = _backgroundLayer.anchoredPosition;
        
        // キャラクターの初期位置
        _characterInitialPos = new Vector2[_characterLayers.Length];
        for (int i = 0; i < _characterLayers.Length; i++)
        {
            _characterInitialPos[i] = _characterLayers[i].anchoredPosition;
        }
        
        // タイトルの初期位置
        _titleInitialPos = _titleLayer.anchoredPosition;
        
        // すべてのトゥイーンをキル（念のため）
        DOTween.KillAll();
    }

    private void Update()
    {
        // マウスの現在位置から相対的な位置を計算 (-1 ~ 1の範囲)
        Vector2 mousePos = Input.mousePosition;
        Vector2 mouseDelta = new Vector2(
            (mousePos.x - _screenCenter.x) / _screenCenter.x,
            (mousePos.y - _screenCenter.y) / _screenCenter.y
        );
        
        // 背景の移動（逆方向にわずかに動かす）
        Vector2 bgTarget = _backgroundInitialPos - new Vector2(
            mouseDelta.x * _backgroundFactor * 100f,
            mouseDelta.y * _backgroundFactor * 100f
        );
        _backgroundLayer.DOAnchorPos(bgTarget, _smoothTime).SetEase(Ease.OutQuad);
        
        // キャラクターの移動（各キャラクターで異なる動きに）
        for (int i = 0; i < _characterLayers.Length; i++)
        {
            // キャラクターの配置位置によって動く強さを変える
            // 中央のキャラは少なく、端のキャラは大きく
            float characterDepth = (i + 1) * 0.5f; // キャラごとの深度係数
            
            Vector2 charTarget = _characterInitialPos[i] + new Vector2(
                mouseDelta.x * _characterFactorBase * characterDepth * 100f,
                mouseDelta.y * _characterFactorBase * characterDepth * 100f
            );
            _characterLayers[i].DOAnchorPos(charTarget, _smoothTime).SetEase(Ease.OutQuad);
        }
        
        // タイトルテキストの移動
        Vector2 titleTarget = _titleInitialPos + new Vector2(
            mouseDelta.x * _titleFactor * 100f,
            mouseDelta.y * _titleFactor * 100f
        );
        _titleLayer.DOAnchorPos(titleTarget, _smoothTime).SetEase(Ease.OutQuad);
    }

    private void OnDisable()
    {
        DOTween.KillAll(); // TODO: CanvasGroupのアルファ値で表示非表示を切り替えているので方法を考える 
    }
}