using System.Collections.Generic;

/// <summary>
/// バトルに関わるアイテム効果を管理するためのクラス
/// </summary>
public class BattleMediator : IBattleMediator
{
    private readonly List<IItemEffect> _activeEffects = new List<IItemEffect>();
    
    public IDirectionSelector DirectionSelector { get; }
    public IBattleEvaluator BattleEvaluator { get; }
    public IVisualController VisualController { get; }
    public IItemProcessor ItemProcessor { get; }

    public BattleMediator(IDirectionSelector directionSelector, IBattleEvaluator battleEvaluator, IVisualController visualController, IItemProcessor itemProcessor)
    {
        DirectionSelector = directionSelector;
        BattleEvaluator = battleEvaluator;
        VisualController = visualController;
        ItemProcessor = itemProcessor;
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

    /// <summary>
    /// ターンの変わり目にアイテム効果に対する処理を行う
    /// </summary>
    public void UpdateEffects()
    {
        // 効果発動中のアイテムがなければreturn
        if (_activeEffects.Count <= 0)
            return;

        // foreach文の中でListを書き換えないように一度配列にコピーする
        var tmpEffects = _activeEffects.ToArray();
        
        foreach (var effect in tmpEffects)
        {
            effect.UpdateTurn();
            if (effect.IsExpired()) // 効果が切れたか確認して解除処理を行う
            {
                RemoveEffect(effect);
            }
        }

    }
}