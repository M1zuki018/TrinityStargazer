/// <summary>
/// アイテム効果とバトルシステムを繋ぐ仲介用インターフェース
/// </summary>
public interface IBattleMediator
{
    IDirectionDecider DirectionDecider { get; }
    IVisualUpdater VisualUpdater { get; }
    IItemEffecter ItemEffecter { get; }
    
    void RegisterEffect(IItemEffect effect);
    void RemoveEffect(IItemEffect effect);
    void UpdateEffects();
}