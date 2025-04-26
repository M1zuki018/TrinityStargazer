using System;
using System.Collections.Generic;

/// <summary>
/// バトルに関わるアイテム効果を管理するためのクラス
/// </summary>
public class BattleMediator : IBattleMediator
{
    private readonly List<IItemEffect> _activeEffects = new List<IItemEffect>();
    
    public IDirectionDecider DirectionDecider { get; }
    public IBattleJudge BattleJudge { get; }
    public IVisualUpdater VisualUpdater { get; }
    
    public BattleMediator(
        IDirectionDecider directionDecider,
        IBattleJudge battleJudge,
        IVisualUpdater visualUpdater)
    {
        DirectionDecider = directionDecider;
        BattleJudge = battleJudge;
        VisualUpdater = visualUpdater;
    }
    
    public void RegisterEffect(IItemEffect effect)
    {
        _activeEffects.Add(effect);
        effect.Apply(this);
    }
    
    public void RemoveEffect(IItemEffect effect)
    {
        effect.Remove(this);
        _activeEffects.Remove(effect);
    }
    
    public void UpdateEffects()
    {
        // ターン更新時の処理など
        // 例えばターン数が減少したエフェクトの削除など
    }
}