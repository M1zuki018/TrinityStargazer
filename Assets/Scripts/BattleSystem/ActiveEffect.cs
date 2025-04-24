/// <summary>
/// 現在効果発動中のアイテム
/// </summary>
public class ActiveEffect
{
    public ItemTypeEnum Type { get; }
    public int RemainingTurns { get; set; }
    
    public int EffectTargets { get; set; }
        
    public ActiveEffect(ItemTypeEnum type, int turns, int targets)
    {
        Type = type;
        RemainingTurns = turns;
        EffectTargets = targets;
    }
}
