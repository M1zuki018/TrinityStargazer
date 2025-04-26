/// <summary>
/// アイテム効果のインターフェース
/// </summary>
public interface IItemEffect
{
    void Apply(IBattleMediator mediator);
    void Remove(IBattleMediator mediator);
    bool IsExpired(); // 効果が切れたかどうか
    void UpdateTurn(); // ターン経過時の処理
}