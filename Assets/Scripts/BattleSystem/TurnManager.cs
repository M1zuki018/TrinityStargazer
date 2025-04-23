using System;

/// <summary>
/// ターンを管理するクラス
/// </summary>
public class TurnManager
{
    private readonly int _maxTurn = 5; //TODO: 最大ターン数を管理するUtilityクラスを作るなどして管理しやすくする
    private int _currentTurn = 1;
    public int CurrentTurn => _currentTurn;
    public event Action OnGameFinished; // 最大ターンに到達したことを通知する

    /// <summary>
    /// 次のターンに進める
    /// </summary>
    public void NextTurn()
    {
        if (_currentTurn >= _maxTurn) // 最大ターンであれば
        {
            OnGameFinished?.Invoke();
            return;
        }
        _currentTurn++;
    }

    /// <summary>
    /// ターンのテキスト表示の部分に合うような文字列を返す
    /// </summary>
    public string TurnText()
    {
        return $"{_currentTurn.ToString()}/{_maxTurn.ToString()}";
    }
}
