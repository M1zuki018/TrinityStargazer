using System;
using System.Collections.Generic;

/// <summary>
/// 封印のページの効果データ
/// </summary>
public class SealPageEffect : IItemEffect
{
    private readonly int limitCount;
    private int _remainingTurns;
    private List<DirectionEnum> _limitDirections = new List<DirectionEnum>();
    
    public SealPageEffect(int limitCount, int turns)
    {
        this.limitCount = limitCount;
        _remainingTurns = turns;
    }
    
    public void Apply(IBattleMediator mediator)
    {
        var sealedDirections = mediator.DirectionDecider.GetSealedDirections();
         
        // 使用可能な方向のリストを作成
        var availableDirections = new List<DirectionEnum>();
        foreach (DirectionEnum direction in Enum.GetValues(typeof(DirectionEnum)))
        {
            if (!sealedDirections.Contains(direction))
            {
                availableDirections.Add(direction);
            }
        }

        var newSealedDirectionList = new List<DirectionEnum>();
        for (int i = 0; i < limitCount; i++)
        {
            DirectionEnum direction = (DirectionEnum)UnityEngine.Random.Range(0, availableDirections.Count); // 方向を作成 
            newSealedDirectionList.Add(direction);
            availableDirections.Remove(direction); // 選んだ方向は使用可能なリストから外す
        }
        
        _limitDirections = newSealedDirectionList; // コピーして解除のときに使えるようにする

        foreach (DirectionEnum direction in newSealedDirectionList)
        {
            mediator.DirectionDecider.LimitProbability(direction);
        }
    }
    
    public void Remove(IBattleMediator mediator)
    {
        foreach (var direction in _limitDirections)
        {
            mediator.DirectionDecider.RemoveLimitProbability(direction);
        }
        _limitDirections.Clear();
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