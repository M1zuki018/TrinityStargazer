using System;

/// <summary>
/// ターンを管理するクラス
/// </summary>
public class TurnManager
{
    private readonly int _maxTurn = 5; //TODO: 最大ターン数を管理するUtilityクラスを作るなどして管理しやすくする
    private int _currentTurn = 0;
    
    public event Action OnGameFinished; // 最大ターンに到達したことを通知する

    /// <summary>
    /// 次のターンに進める
    /// </summary>
    public void NextTurn()
    {
        _currentTurn++;
        
        if (_currentTurn >= _maxTurn)
        {
            OnGameFinished?.Invoke();
        }
    }
}
