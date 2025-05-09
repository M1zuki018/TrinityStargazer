using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゲームを閉じる画面のキャンバスコントローラー
/// </summary>
public class CanvasController_Quit : WindowBase
{
    [SerializeField] private Button _yesButton;
    [SerializeField] private Button _noButton;

    public event Action OnNoButtonClicked;

    public override UniTask OnAwake()
    {
        if (_yesButton != null) _yesButton.onClick.AddListener(Quit);
        if(_noButton != null) _noButton.onClick.AddListener(OnNoButtonClick);
        
        return base.OnAwake();
    }
    
    /// <summary>
    /// やめないボタンをクリックした時
    /// </summary>
    private void OnNoButtonClick() => OnNoButtonClicked?.Invoke();
    
    /// <summary>
    /// ゲーム終了処理
    /// </summary>
    private void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
      Application.Quit();
#endif
    }

    private void OnDestroy()
    {
        if (_yesButton != null) _yesButton.onClick?.RemoveAllListeners();
        if(_noButton != null) _noButton.onClick?.RemoveAllListeners();
    }
}
