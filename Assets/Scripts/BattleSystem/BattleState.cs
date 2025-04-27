/// <summary>
/// バトルの状態を管理するクラス
/// </summary>
public class BattleState
{
    private bool _isWinLastBattle; // 前回のバトルで勝利したか
    private int _victoryCount; // 勝利数
    private int _victoryPointValue = 1; // 勝利時に獲得するポイント数（初期値は1。アイテム決闘の薔薇の効果で変動する）
    private bool _isVictory;
    public bool IsVictory => _isVictory;

    /// <summary>
    /// 勝利ポイントを記録する
    /// </summary>
    public void ProcessBattleResult(bool isVictory)
    {
        _isVictory = isVictory;
        
        if (IsVictory)
        {
            _victoryCount += _victoryPointValue;
            _isWinLastBattle = true;
        }
        else
        {
            _isWinLastBattle = false;
        }
    }
    
    /// <summary>
    /// 勝利数が最大ターン数を上回っているか確認し、上回っている場合trueを返す
    /// </summary>
    public bool IsVictoryConditionMet()
    {
        return _victoryCount >= GameManagerServiceLocator.Instance.GetGameModeData().MaxTurn;
    }
    
    /// <summary>
    /// 勝利した時に得られるポイント数を変更する
    /// </summary>
    public void SetVictoryPointValue(int pointValue)
    {
        _victoryPointValue = pointValue;
    }
    
    /// <summary>
    /// 前回のバトルで勝っていた場合は勝利数を変更する処理を行う
    /// </summary>
    public void RevertLastBattleResult()
    {
        if (_isWinLastBattle)
        {
            _victoryCount -= _victoryPointValue;
        }
    }
}
