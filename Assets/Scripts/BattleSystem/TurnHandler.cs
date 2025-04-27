using System;

/// <summary>
/// ターンを管理するクラス
/// </summary>
public class TurnHandler
{
    private readonly int _maxTurn = GameManagerServiceLocator.Instance.GetGameModeData().MaxTurn;
    private int _currentTurn = 1;
    public int CurrentTurn => _currentTurn;
    public event Action OnGameFinished; // 最大ターンに到達したことを通知する
    
    /// <summary>
    /// 次のターンに進める
    /// </summary>
    public void AdvanceToNextTurn()
    {
        if (_currentTurn >= _maxTurn) // 最大ターンであれば
        {
            OnGameFinished?.Invoke();
            return;
        }
        _currentTurn++;
    }

    /// <summary>
    /// ターンを一つ戻す
    /// </summary>
    public void RevertTurn()
    {
        if (_currentTurn > 1) // 0ターンにはならないようにする
        {
            _currentTurn--;
        }
    }

    /// <summary>
    /// 残りのターンを飛ばしてゲーム終了イベントを発火
    /// </summary>
    public void CompleteBattle()
    {
        OnGameFinished?.Invoke();
    }

    /// <summary>
    /// ターンのテキスト表示の部分に合うような文字列を返す
    /// </summary>
    public string GetTurnText()
    {
        return $"{_currentTurn.ToString()}/{_maxTurn.ToString()}";
    }
}
