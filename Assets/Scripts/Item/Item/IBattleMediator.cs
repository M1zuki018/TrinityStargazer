/// <summary>
/// アイテム効果とバトルシステムを繋ぐ仲介用インターフェース
/// </summary>
public interface IBattleMediator
{
    IItemEffecter ItemEffecter { get; }
    
    void RegisterEffect(IItemEffect effect);
    void RemoveEffect(IItemEffect effect);
    void UpdateEffects();
}