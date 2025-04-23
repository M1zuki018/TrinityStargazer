using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// AfterPanelのキャンバスコントローラー
/// </summary>
public class CanvasController_After : WindowBase
{
    [SerializeField] private Button _nextButton;
    
    public event Action OnNextButtonClicked;

    public override UniTask OnUIInitialize()
    {
        if (_nextButton != null) _nextButton.onClick.AddListener(OnNextButtonClick);
        return base.OnUIInitialize();
    }
    
    /// <summary>
    /// パネルを閉じて次のバトルへ
    /// </summary>
    private void OnNextButtonClick() => OnNextButtonClicked?.Invoke();

    private void OnDestroy()
    {
        if (_nextButton != null) _nextButton?.onClick.RemoveAllListeners();
    }
}
