using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// タイトル画面のキャンバスコントローラー
/// </summary>
public class CanvasController_Title : WindowBase
{
    [SerializeField] private Button _startButton;
    public event Action OnHomeButtonClicked;
    
    public  override UniTask OnAwake()
    {
        if(_startButton != null) _startButton.onClick.AddListener(GameStart);
        return base.OnAwake();
    }
    
    /// <summary>
    /// ゲーム開始→ホーム画面に遷移するボタン
    /// </summary>
    private void GameStart()
    {
        OnHomeButtonClicked?.Invoke();
        GameManager.Instance.ChangeGameState(GameStateEnum.Home);
    }

    private void OnDestroy()
    {
        if(_startButton != null) _startButton.onClick?.RemoveAllListeners();
    }
}
