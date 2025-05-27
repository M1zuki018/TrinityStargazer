using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 勝敗数を表すマークを管理するクラス
/// </summary>
public class ResultMark : ViewBase
{
    [SerializeField] private GameObject _markPrefab;
    [SerializeField] private Color _defalutColor;
    private List<Image> _marks = new List<Image>();
    
    public override UniTask OnUIInitialize()
    {
        int maxTurn = GameManagerServiceLocator.Instance.GetGameModeData().MaxTurn;
        for (int i = 0; i < maxTurn; i++)
        {
            // 子オブジェクトにマークを生成（LayoutGroupがついているので自動整列）
            Image mark = Instantiate(_markPrefab, transform).GetComponent<Image>();
            _marks.Add(mark);
            mark.color = _defalutColor;
        }
        
        return base.OnUIInitialize();
    }

    /// <summary>
    /// マークの表示を更新する
    /// </summary>
    public void MarkUpdate(int turn, bool isVictory)
    {
        _marks[turn].color = isVictory ? Color.red : Color.blue;
    }

    /// <summary>
    /// マークの色をリセットする
    /// </summary>
    public void ResetMark(int turn)
    {
        _marks[turn].color = _defalutColor;
    }
}
