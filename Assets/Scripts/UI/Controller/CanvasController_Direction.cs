using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 方向選択画面のキャンバスコントローラー
/// </summary>
public class CanvasController_Direction : WindowBase
{
    [SerializeField] private Button[] _directionButtons = new Button[8];
    public Button[] DirectionButtons => _directionButtons; // アイテム効果をUIに反映させるために公開する
    
    public event Action<DirectionEnum> OnDirectionButtonClicked;
    
    public override UniTask OnUIInitialize()
    {
        if (_directionButtons != null)
        {
            for (int i = 0; i < _directionButtons.Length; i++)
            {
                int index = i;
                _directionButtons[i].onClick.AddListener(() => OnDirectionButtonClick((DirectionEnum)index));
            }
        } 
        return base.OnUIInitialize();
    }

    /// <summary>
    /// 方向決定ボタンを押した時の処理（アイテム効果で外部から呼び出す必要がある）
    /// </summary>
    public void OnDirectionButtonClick(DirectionEnum direction)
    {
        OnDirectionButtonClicked?.Invoke(direction);
    }

    private void OnDestroy()
    {
        if (_directionButtons != null)
        {
            foreach (var directionButton in _directionButtons)
            {
                directionButton.onClick?.RemoveAllListeners();
            }
        }
    }
}
