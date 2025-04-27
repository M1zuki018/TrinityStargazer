using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ターン表示に関するUIを管理するクラス
/// </summary>
public class TurnUIs : ViewBase
{
    [SerializeField] private Text _turnText;
    [SerializeField] private ResultMark _resultMark;
    [SerializeField] private Text _modeText;

    public override UniTask OnUIInitialize()
    {
        SetModeText();
        return base.OnUIInitialize();
    }
    
    /// <summary>
    /// ターンの表示を更新する
    /// </summary>
    public void SetTurnText(string text) => _turnText.text = $"ターン　{text}";
    
    /// <summary>
    /// 勝敗の表示を更新する
    /// </summary>
    public void SetResultMark(int turn, bool isVictory) => _resultMark.MarkUpdate(turn, isVictory);

    /// <summary>
    /// 指定した勝敗の表示をリセットする
    /// </summary>
    public void ResetResultMark(int turn) => _resultMark.ResetMark(turn);
    
    /// <summary>
    /// 対戦モードの表示を更新する
    /// </summary>
    private void SetModeText() => _modeText.text = $"対戦モード　{GameManagerServiceLocator.Instance.GetGameModeData().GameModeName}";
}
