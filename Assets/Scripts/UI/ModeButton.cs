using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ゲームモード選択ボタン
/// </summary>
public class ModeButton : ViewBase
{
    [SerializeField] private GameModeEnum _mode;
    [SerializeField] private Button _button;
    
    public GameModeEnum Mode => _mode;
    public Button Button => _button;
    
    public override UniTask OnUIInitialize()
    {
        if(_button == null) _button = GetComponent<Button>();
        return base.OnUIInitialize();
    }
}
