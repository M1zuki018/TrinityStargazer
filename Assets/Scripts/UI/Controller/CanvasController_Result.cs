using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// リザルト画面のキャンバスコントローラー
/// </summary>
public class CanvasController_Result : WindowBase
{
    [SerializeField] private Button _homeButton;
    
    public event Action OnHomeButtonClicked;

    public override UniTask OnUIInitialize()
    {
        if(_homeButton != null) _homeButton.onClick.AddListener(OnHomeButtonClick);
        return base.OnUIInitialize();
    }

    /// <summary>
    /// メインシーンへ戻る
    /// </summary>
    private void OnHomeButtonClick() => OnHomeButtonClicked?.Invoke();

    private void OnDestroy()
    {
        if(_homeButton != null) _homeButton.onClick?.RemoveListener(OnHomeButtonClick);
    }
}
