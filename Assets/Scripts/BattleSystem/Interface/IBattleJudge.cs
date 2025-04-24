/// <summary>
/// 勝敗を判定するインターフェース
/// </summary>
public interface IBattleJudge
{
    bool Judge(DirectionEnum enemyDirection, DirectionEnum playerDirection);
}