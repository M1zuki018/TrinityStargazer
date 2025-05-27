using UnityEngine;

/// <summary>
/// バトルを管理するマネージャークラス
/// </summary>
public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;
    
    [SerializeField][ExpandableSO] private BattleModeSO _modeSO;
    
    public int VictoryPoints {get; private set;}
    
    /// <summary>
    /// 勝利数をセットする
    /// </summary>
    public int SetVictoryPoints(int points) => VictoryPoints = points;

    /// <summary>
    /// 現在選択中のモードのデータを取得する
    /// </summary>
    public BattleModeData GetGameModeData() => 
        _modeSO.GetModeData(GameManagerServiceLocator.Instance.CurrentGameMode);
}
