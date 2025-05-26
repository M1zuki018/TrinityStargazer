using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

/// <summary>
/// まばたきアニメーションを管理するクラス
/// </summary>
public class BlinkAnimation : MonoBehaviour
{
    [SerializeField] private Sprite[] _blinkSprites; // まばたきのスプライト
    [SerializeField] private float _blinkDuration = 0.15f;
    [SerializeField] private Vector2 _blinkIntervalRange = new Vector2(3f, 7f);
    
    private Image _image;
    private CancellationTokenSource _cancellationTokenSource;
    private bool _isBlinking;

    private void Start()
    {
        _image = GetComponent<Image>();
        _cancellationTokenSource = new CancellationTokenSource();
        StartBlinkLoop(_cancellationTokenSource.Token).Forget();
    }
    
    private async UniTaskVoid StartBlinkLoop(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            // ランダムな間隔で待機
            float waitTime = Random.Range(_blinkIntervalRange.x, _blinkIntervalRange.y);
            await UniTask.Delay(TimeSpan.FromSeconds(waitTime), cancellationToken: cancellationToken);
            
            if (!_isBlinking)
            {
                await ExecuteBlink(cancellationToken);
            }
        }
    }

    /// <summary>
    /// まばたきアニメーションを実行する
    /// </summary>
    private async UniTask ExecuteBlink(CancellationToken cancellationToken)
    {
        _isBlinking = true;
        
        try
        {
            float frameTime = _blinkDuration / (_blinkSprites.Length - 1);
            
            // 閉じるアニメーション
            for (int i = 1; i < _blinkSprites.Length; i++)
            {
                _image.sprite = _blinkSprites[i];
                await UniTask.Delay(TimeSpan.FromSeconds(frameTime), cancellationToken: cancellationToken);
            }
            
            // 開くアニメーション
            for (int i = _blinkSprites.Length - 2; i >= 0; i--)
            {
                _image.sprite = _blinkSprites[i];
                await UniTask.Delay(TimeSpan.FromSeconds(frameTime), cancellationToken: cancellationToken);
            }
        }
        finally
        {
            _isBlinking = false;
        }
    }

    private void OnDestroy()
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();
    }
}
