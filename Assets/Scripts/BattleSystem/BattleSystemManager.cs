/// <summary>
/// インゲームのバトルを統括管理するクラス
/// </summary>
public class BattleSystemManager : IBattleSystem
{
    private readonly IBattleMediator _mediator; // バトルに必要な機能が入ったインターフェースなどはこのクラスで管理
    public IBattleMediator Mediator => _mediator;
    public bool IsVictory { get; private set; }

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
    /// 方向の確率を調整
    /// </summary>
    public void ModifyDirectionProbability(DirectionEnum direction, float addedProbability)
    {
        _mediator.DirectionDecider.ModifyProbability(direction, addedProbability);
    }

    /// <summary>
    /// 方向に制限をかける
    /// </summary>
    private void LimitProbability(DirectionEnum direction)
    {
        _mediator.DirectionDecider.LimitProbability(direction);
    }
    
    /// <summary>
    /// 方向の制限を解除する
    /// </summary>
    private void RemoveLimitProbability(DirectionEnum direction)
    {
        _mediator.DirectionDecider.RemoveLimitProbability(direction);
    }
}