/// <summary>
/// アイテム効果とバトルシステムを繋ぐ仲介用インターフェース
/// </summary>
public interface IBattleMediator
{
    IDirectionSelector DirectionSelector { get; }
    IBattleEvaluator BattleEvaluator { get; }
    IVisualController VisualController { get; }
    IItemProcessor ItemProcessor { get; }
    
    void RegisterEffect(IItemEffect effect);
    void RemoveEffect(IItemEffect effect);
    void UpdateEffects();
}