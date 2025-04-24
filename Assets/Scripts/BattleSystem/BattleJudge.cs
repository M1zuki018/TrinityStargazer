/// <summary>
/// バトルの勝敗判定を担当するクラス
/// </summary>
public class BattleJudge : IBattleJudge
{
    /// <summary>
    /// 敵と自分の方向から勝敗を判定する
    /// </summary>
    public bool Judge(DirectionEnum enemyDirection, DirectionEnum playerDirection)
    {
        return enemyDirection == playerDirection;
    }
}