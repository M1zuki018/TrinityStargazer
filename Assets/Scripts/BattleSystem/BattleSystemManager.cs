using System;

/// <summary>
/// インゲームのバトルを統括管理するクラス
/// </summary>
public class BattleSystemManager : IBattleSystem, IDisposable
{
    private readonly IDirectionDecider _directionDecider;
    private readonly IBattleJudge _battleJudge;
    private readonly IVisualUpdater _visualUpdater;
    private readonly IItemManager _itemManager;
    
    public bool IsVictory { get; private set; }

    public BattleSystemManager(
        IDirectionDecider directionDecider, 
        IBattleJudge battleJudge,
        IVisualUpdater visualUpdater,
        IItemManager itemManager)
    {
        _directionDecider = directionDecider;
        _battleJudge = battleJudge;
        _visualUpdater = visualUpdater;
        _itemManager = itemManager;
        
        BindItemManager();
    }

    /// <summary>
    /// アイテムマネージャーとバトルシステムを繋ぐ
    /// </summary>
    private void BindItemManager()
    {
        _itemManager.UseLimitItem += LimitProbability;
        _itemManager.RemoveLimitItem += RemoveLimitProbability;
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

    /// <summary>
    /// 方向に制限をかける
    /// </summary>
    private void LimitProbability(DirectionEnum direction)
    {
        _directionDecider.LimitProbability(direction);
    }
    
    /// <summary>
    /// 方向の制限を解除する
    /// </summary>
    private void RemoveLimitProbability(DirectionEnum direction)
    {
        _directionDecider.RemoveLimitProbability(direction);
    }

    public void Dispose()
    {
        _itemManager.UseLimitItem -= LimitProbability;
        _itemManager.RemoveLimitItem -= RemoveLimitProbability;
    }
}