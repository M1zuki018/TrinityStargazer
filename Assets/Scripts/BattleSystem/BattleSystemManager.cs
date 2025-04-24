using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

/// <summary>
/// インゲームのバトルを管理するクラス
/// </summary>
public class BattleSystemManager
{
    // 各方向を向く確率
    private Dictionary<DirectionEnum, float> _directionProbabilities = new Dictionary<DirectionEnum, float>
    {
        { DirectionEnum.Up, 0.125f },
        { DirectionEnum.UpRight, 0.125f },
        { DirectionEnum.Right, 0.125f },
        { DirectionEnum.DownRight, 0.125f },
        { DirectionEnum.Down, 0.125f },
        { DirectionEnum.DownLeft, 0.125f },
        { DirectionEnum.Left, 0.125f },
        { DirectionEnum.UpLeft, 0.125f }
    };
    private DirectionalImages _seiImage, _playerHandImage;

    public bool IsVictory { get; private set; }

    public BattleSystemManager(DirectionalImages seiImage, DirectionalImages playerHandImage)
    {
        _seiImage = seiImage;
        _playerHandImage = playerHandImage;
    }

    /// <summary>
    ///  外部から各方向を向く確率を変更するためのメソッド
    /// </summary>
    public void ModifyDirectionProbability(DirectionEnum direction, float addedProbability)
    {
        float total = _directionProbabilities.Values.Sum(); // 合計
        
        _directionProbabilities[direction] += addedProbability; // 選択された方向の確率を更新
        
        float newTotal = _directionProbabilities.Values.Sum(); // 新しい合計を計算
    
        // 正規化
        float normalizeFactor = total / newTotal;
        foreach (var dir in _directionProbabilities.Keys.ToList())
        {
            _directionProbabilities[dir] *= normalizeFactor;
        }
    }

    /// <summary>
    /// 敵/自分の手の向きを決める
    /// </summary>
    public void SetSprite(DirectionEnum direction)
    {
        DirectionEnum decisionDirection = DecisionDirection();
        _seiImage.SetSprite(decisionDirection); // 敵の向く方向を決めて指定する
        _playerHandImage.SetSprite(direction);
        
        IsVictory = decisionDirection == direction; // 勝敗
    }

     /// <summary>
    /// 敵が向く方向を決める
    /// </summary>
    private DirectionEnum DecisionDirection()
    {
        float rand = Random.value; // 0~1の乱数を生成
        float cumulativeProbability = 0f;
        
        cumulativeProbability += _directionProbabilities[DirectionEnum.Up];
        if (rand < cumulativeProbability)
        {
            return DirectionEnum.Up;
        }
        
        cumulativeProbability += _directionProbabilities[DirectionEnum.UpRight];
        if (rand < cumulativeProbability)
        {
            return DirectionEnum.UpRight;
        }
        
        cumulativeProbability += _directionProbabilities[DirectionEnum.Right];
        if (rand < cumulativeProbability)
        {
            return DirectionEnum.Right;
        }
        
        cumulativeProbability += _directionProbabilities[DirectionEnum.DownRight];
        if (rand < cumulativeProbability)
        {
            return DirectionEnum.DownRight;
        }
        
        cumulativeProbability += _directionProbabilities[DirectionEnum.Down];
        if (rand < cumulativeProbability)
        {
            return DirectionEnum.Down;
        }
        
        cumulativeProbability += _directionProbabilities[DirectionEnum.DownLeft];
        if (rand < cumulativeProbability)
        {
            return DirectionEnum.DownLeft;
        }
        
        cumulativeProbability += _directionProbabilities[DirectionEnum.Left];
        if (rand < cumulativeProbability)
        {
            return DirectionEnum.Left;
        }
        
        // ここまで来たら最後の方向（UpLeft）
        return DirectionEnum.UpLeft;
    }
    
    /// <summary>
    /// 各方向の確率をリセットする
    /// </summary>
    public void ResetDirectionProbabilities()
    {
        foreach (var dir in _directionProbabilities.Keys.ToList())
        {
            _directionProbabilities[dir] = 0.125f;
        }
    }
}
