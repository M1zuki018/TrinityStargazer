using System;
using System.Collections.Generic;

/// <summary>
/// 共鳴ケーブルの効果データ
/// </summary>
public class ResonanceCableEffect : IItemEffect
{
    private int _limitCount;
    private int _remainingTurns;

    public ResonanceCableEffect(int limitCount, int remainingTurns)
    {
        _limitCount = limitCount;
        _remainingTurns = remainingTurns;
    }
    
    public void Apply(IBattleMediator mediator)
    {
        // 使用可能な方向のリストを作成
        var availableDirections = new List<DirectionEnum>();
        foreach (DirectionEnum direction in Enum.GetValues(typeof(DirectionEnum)))
        {
            availableDirections.Add(direction);
        }
        
        var directions = new List<DirectionEnum>(_limitCount);
        for (int i = 0; i < _limitCount; i++)
        {
            int randomIndex = UnityEngine.Random.Range(0, availableDirections.Count);
            DirectionEnum direction = availableDirections[randomIndex];
        
            directions.Add(direction);
            availableDirections.RemoveAt(randomIndex); // 選んだ方向は選択可能なリストから外しておく
        }
        mediator.ItemEffecter.UseResonanceCable(directions,this); // リンクさせる処理を呼ぶ
    }

    public void Remove(IBattleMediator mediator)
    {
        mediator.ItemEffecter.ReleasingDirection(this);
    }

    public bool IsExpired()
    {
        return _remainingTurns <= 0;
    }

    public void UpdateTurn()
    {
        _remainingTurns--;
    }
}
