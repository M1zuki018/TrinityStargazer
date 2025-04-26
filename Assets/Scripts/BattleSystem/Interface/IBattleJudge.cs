using System;
using System.Collections.Generic;

/// <summary>
/// 勝敗を判定するインターフェース
/// </summary>
public interface IBattleJudge
{
    event Action<DirectionEnum> OnLink;
    event Action<DirectionEnum> OnRelease;
    
    bool Judge(DirectionEnum enemyDirection, DirectionEnum playerDirection);
    void LinkingDirection(List<DirectionEnum> directions);
    void ReleasingDirection();
}