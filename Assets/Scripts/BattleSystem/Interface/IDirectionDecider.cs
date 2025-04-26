using System;

/// <summary>
/// 方向を決定するインターフェース
/// </summary>
public interface IDirectionDecider
{
     DirectionEnum DecideDirection();
    void ModifyProbability(DirectionEnum direction, float addedProbability);
    void LimitProbability(DirectionEnum direction);
    void RemoveLimitProbability(DirectionEnum direction);
    void ResetProbabilities();
    
    event Action<DirectionEnum> OnLimitedDirection;
    event Action<DirectionEnum> OnUnlimitedDirection;
}