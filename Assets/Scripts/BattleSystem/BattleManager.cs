using UnityEngine;

/// <summary>
/// バトルを管理するマネージャークラス
/// </summary>
public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;
    
    [SerializeField][ExpandableSO] private BattleModeSO _modeSO;
    [SerializeField] private GameModeEnum _currentGameMode = GameModeEnum.Normal;
    
    public int VictoryPoints {get; private set;}
    
    /// <summary>
    /// ゲームモードを変更する
    /// </summary>
    public void SetGameMode(GameModeEnum mode) => _currentGameMode = mode;
    
    /// <summary>
    /// 勝利数をセットする
    /// </summary>
    public int SetVictoryPoints(int points) => VictoryPoints = points;

    /// <summary>
    /// 現在選択中のモードのデータを取得する
    /// </summary>
    public BattleModeData GetGameModeData() => _modeSO.GetModeData(_currentGameMode);
}
