/// <summary>
/// インゲームのバトルを統括管理するクラス
/// </summary>
public class BattleSystemManager
{
    private readonly IBattleMediator _mediator; // バトルに必要な機能が入ったインターフェースなどはこのクラスで管理
    public IBattleMediator Mediator => _mediator;
    public bool IsVictory { get; private set; }

    public BattleSystemManager(
        IDirectionDecider directionDecider, 
        IBattleJudge battleJudge,
        IVisualUpdater visualUpdater,
        BattleSystemPresenter battleSystemPresenter)
    {
        _mediator = new BattleMediator(directionDecider, battleJudge, visualUpdater, battleSystemPresenter);
    }

    /// <summary>
    /// 敵が向く方向を決定して返す
    /// </summary>
    public DirectionEnum EnemyDirection()
    {
        return _mediator.DirectionDecider.DecideDirection();
    }
    
    /// <summary>
    /// バトルの実行
    /// </summary>
    public void ExecuteBattle(DirectionEnum playerDirection, DirectionEnum enemyDirection)
    {
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
}