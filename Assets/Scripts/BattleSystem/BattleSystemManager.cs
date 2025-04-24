/// <summary>
/// インゲームのバトルを統括管理するクラス
/// </summary>
public class BattleSystemManager : IBattleSystem
{
    private readonly IDirectionDecider _directionDecider;
    private readonly IBattleJudge _battleJudge;
    private readonly IVisualUpdater _visualUpdater;
    
    public bool IsVictory { get; private set; }

    public BattleSystemManager(
        IDirectionDecider directionDecider, 
        IBattleJudge battleJudge,
        IVisualUpdater visualUpdater)
    {
        _directionDecider = directionDecider;
        _battleJudge = battleJudge;
        _visualUpdater = visualUpdater;
    }

    /// <summary>
    /// バトルの実行
    /// </summary>
    public void ExecuteBattle(DirectionEnum playerDirection)
    {
        DirectionEnum enemyDirection = _directionDecider.DecideDirection();
        _visualUpdater.UpdateSprites(enemyDirection, playerDirection);
        IsVictory = _battleJudge.Judge(enemyDirection, playerDirection);
    }
    
    /// <summary>
    /// バトルの初期化
    /// </summary>
    public void ResetBattle()
    {
        _directionDecider.ResetProbabilities();
        _visualUpdater.ResetSprites();
    }
    
    /// <summary>
    /// 方向の確率を調整
    /// </summary>
    public void ModifyDirectionProbability(DirectionEnum direction, float addedProbability)
    {
        _directionDecider.ModifyProbability(direction, addedProbability);
    }
}