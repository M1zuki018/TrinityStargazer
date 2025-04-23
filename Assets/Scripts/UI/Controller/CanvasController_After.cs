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
    [SerializeField] private Text _resultText;
    
    public event Action OnNextButtonClicked;

    public override UniTask OnUIInitialize()
    {
        if (_nextButton != null) _nextButton.onClick.AddListener(OnNextButtonClick);
        if(_resultText != null) _resultText.text = "";
        
        return base.OnUIInitialize();
    }
    
    /// <summary>
    /// パネルを閉じて次のバトルへ
    /// </summary>
    private void OnNextButtonClick() => OnNextButtonClicked?.Invoke();

    /// <summary>
    /// テキストを更新する
    /// </summary>
    public void SetText(bool isVictory)
    {
        if (_resultText != null)
        {
            _resultText.text = isVictory ? "勝ち！" : "負け...";
        }
    }

    private void OnDestroy()
    {
        if (_nextButton != null) _nextButton?.onClick.RemoveAllListeners();
    }
}
