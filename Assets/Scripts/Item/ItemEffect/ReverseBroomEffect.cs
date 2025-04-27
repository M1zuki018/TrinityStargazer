/// <summary>
/// 逆行のほうきの効果データ
/// </summary>
public class ReverseBroomEffect : IItemEffect
{
    private int _remainingTurns = 1; // 効果持続時間 = 1ターン
    
    public void Apply(IBattleMediator mediator)
    {
        mediator.ItemProcessor.UseReverseBroom();
    }

    public void Remove(IBattleMediator mediator)
    {
        // 特に処理はない
    }

    public bool IsExpired()
    {
        return _remainingTurns <= 0;
    }

    public void UpdateTurn()
    {
        _remainingTurns--;
    }
}
