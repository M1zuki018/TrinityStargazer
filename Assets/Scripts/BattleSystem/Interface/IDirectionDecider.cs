/// <summary>
/// 方向を決定するインターフェース
/// </summary>
public interface IDirectionDecider
{
    DirectionEnum DecideDirection();
    void ModifyProbability(DirectionEnum direction, float addedProbability);
    void LimitProbability(DirectionEnum direction);
    void ResetProbabilities();
}