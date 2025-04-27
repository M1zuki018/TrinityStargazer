using UnityEngine;

/// <summary>
/// とっておきの効果データ
/// </summary>
public class StarAttractionEffect : IItemEffect
{
    private readonly float _value;
    private int _remainingTurns;
    DirectionEnum _effectDirection;
    
    public StarAttractionEffect(int value, int remainingTurns)
    {
        _value = value * 0.01f; // 使用時の形に単位を整える
        _remainingTurns = remainingTurns;
    }
    
    public void Apply(IBattleMediator mediator)
    {
        _effectDirection = (DirectionEnum)Random.Range(0, 8);
        mediator.ItemEffecter.ModifyProbability(_effectDirection, _value);
        mediator.ItemEffecter.ChangeButtonColor(_effectDirection, Color.magenta);
        Debug.Log($"[とっておき] 効果発動中 {_effectDirection}");
    }

    public void Remove(IBattleMediator mediator)
    {
        mediator.ItemEffecter.ResetProbabilities(); // 値をもとに戻す
        mediator.ItemEffecter.ResetButtonColor(_effectDirection);
        Debug.Log($"[とっておき] 解除");
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
