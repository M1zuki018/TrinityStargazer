using System.Collections.Generic;

/// <summary>
/// 勝敗を判定するインターフェース
/// </summary>
public interface IBattleJudge
{
    bool Judge(DirectionEnum enemyDirection, DirectionEnum playerDirection);
    void LinkingDirection(List<DirectionEnum> directions);
    void ReleasingDirection();
}