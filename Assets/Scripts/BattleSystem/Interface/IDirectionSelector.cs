using System;
using System.Collections.Generic;

/// <summary>
/// 方向を決定するインターフェース
/// </summary>
public interface IDirectionSelector
{
     DirectionEnum DecideDirection();
     HashSet<DirectionEnum> GetSealedDirections();
    void ModifyProbability(DirectionEnum direction, float addedProbability);
    void LimitProbability(DirectionEnum direction);
    void RemoveLimitProbability(DirectionEnum direction);
    void ResetProbabilities();
    
    event Action<DirectionEnum> OnLimitedDirection;
    event Action<DirectionEnum> OnUnlimitedDirection;
    
    event Action<DirectionEnum> OnEnemyDirectionChanged;
}