using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 勝敗を判定するインターフェース
/// </summary>
public interface IBattleEvaluator
{
    event Action<DirectionEnum, Color> OnLink;
    event Action<DirectionEnum> OnRelease;
    
    bool Judge(DirectionEnum enemyDirection, DirectionEnum playerDirection);
    void LinkingDirection(List<DirectionEnum> directions, ResonanceCableEffect effect);
    void ReleasingDirection(ResonanceCableEffect effect);
}