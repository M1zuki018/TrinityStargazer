using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// バトルの勝敗判定を担当するクラス
/// </summary>
public class BattleJudge : IBattleJudge
{
    // 同一のものとして扱う方向
    private List<DirectionEnum> _linkingDirections = new List<DirectionEnum>();
    public event Action<DirectionEnum> OnLink;
    public event Action<DirectionEnum> OnRelease;

    /// <summary>
    /// 敵と自分の方向から勝敗を判定する
    /// </summary>
    public bool Judge(DirectionEnum enemyDirection, DirectionEnum playerDirection)
    {
        // 何かしらリンクされている場合かつ、リンクされている方向を敵が選択している場合はif文内で判定を行う
        if (_linkingDirections.Count > 0 && _linkingDirections.Contains(enemyDirection))
        {
            bool judge = false;
            foreach (var direction in _linkingDirections)
            {
                Debug.Log("[共鳴ケーブル] 探索");
                if (direction == playerDirection)
                {
                    judge = true;
                };
            }
            return judge;
        }
        
        return enemyDirection == playerDirection;
    }
    
    /// <summary>
    /// アイテム：共鳴ケーブルの効果で、渡された方向を同一のものとして扱う
    /// </summary>
    public void LinkingDirection(List<DirectionEnum> directions)
    {
        _linkingDirections.Clear(); // 念のためクリアしておく
        _linkingDirections = directions;
        foreach (var direction in directions)
        {
            OnLink?.Invoke(direction);
        }
        Debug.Log("[共鳴ケーブル] リンク中");
    }

    /// <summary>
    /// アイテム：共鳴ケーブルの効果を解除する
    /// </summary>
    public void ReleasingDirection()
    {
        foreach (var direction in _linkingDirections)
        {
            OnRelease?.Invoke(direction);
        }
        _linkingDirections.Clear();
        Debug.Log("[共鳴ケーブル] Release");
    }
}