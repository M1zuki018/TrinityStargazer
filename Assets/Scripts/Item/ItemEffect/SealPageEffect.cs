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
        var sealedDirections = mediator.ItemEffecter.GetSealedDirections();
         
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
            int randomIndex = UnityEngine.Random.Range(0, availableDirections.Count);
            DirectionEnum direction = availableDirections[randomIndex];
        
            newSealedDirectionList.Add(direction);
            availableDirections.RemoveAt(randomIndex); // 選んだ方向は選択可能なリストから外しておく
        }
        
        _limitDirections = newSealedDirectionList; // コピーして解除のときに使えるようにする

        foreach (DirectionEnum direction in newSealedDirectionList)
        {
            mediator.ItemEffecter.LimitProbability(direction);
        }
    }
    
    public void Remove(IBattleMediator mediator)
    {
        foreach (var direction in _limitDirections)
        {
            mediator.ItemEffecter.RemoveLimitProbability(direction);
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