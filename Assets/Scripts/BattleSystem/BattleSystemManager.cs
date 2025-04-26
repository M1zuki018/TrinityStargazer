using System.Collections.Generic;

/// <summary>
/// インゲームのバトルを統括管理するクラス
/// </summary>
public class BattleSystemManager
{
    private readonly IBattleMediator _mediator; // バトルに必要な機能が入ったインターフェースなどはこのクラスで管理
    public IBattleMediator Mediator => _mediator;
    public bool IsVictory { get; private set; }
    
    private List<IItemEffect> _itemEffects = new List<IItemEffect>(10); // 初期値としてある程度リストを確保しておく

    public BattleSystemManager(
        IDirectionDecider directionDecider, 
        IBattleJudge battleJudge,
        IVisualUpdater visualUpdater)
    {
        _mediator = new BattleMediator(directionDecider, battleJudge, visualUpdater);
    }
    
    /// <summary>
    /// バトルの実行
    /// </summary>
    public void ExecuteBattle(DirectionEnum playerDirection)
    {
        DirectionEnum enemyDirection = _mediator.DirectionDecider.DecideDirection();
        _mediator.VisualUpdater.UpdateSprites(enemyDirection, playerDirection);
        IsVictory = _mediator.BattleJudge.Judge(enemyDirection, playerDirection);
    }
    
    /// <summary>
    /// バトルの初期化
    /// </summary>
    public void ResetBattle()
    {
        _mediator.DirectionDecider.ResetProbabilities();
        _mediator.VisualUpdater.ResetSprites();
    }

    /// <summary>
    /// 発動中のアイテム効果を追加する
    /// </summary>
    public void AddItemEffect(IItemEffect effect)
    {
        _itemEffects.Add(effect);
    }
}