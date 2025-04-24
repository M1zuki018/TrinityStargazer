/// <summary>
/// BattleSystem用のインターフェース
/// </summary>
public interface IBattleSystem
{
    public void ModifyDirectionProbability(DirectionEnum direction, float addedProbability);
}
