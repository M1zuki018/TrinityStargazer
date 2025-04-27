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
    // アイテムの複数使用に対応するための辞書
    private Dictionary<ResonanceCableEffect, List<DirectionEnum>> _effects = new Dictionary<ResonanceCableEffect, List<DirectionEnum>>();
    public event Action<DirectionEnum, Color> OnLink;
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
        
        return enemyDirection == playerDirection; // 通常の判定　一致しているかどうかを判定
    }

    #region アイテムの処理
    
    /// <summary>
    /// アイテム：共鳴ケーブルの効果で、渡された方向を同一のものとして扱う
    /// </summary>
    public void LinkingDirection(List<DirectionEnum> directions, ResonanceCableEffect effect)
    {
        _effects.Add(effect, directions); // 辞書に効果を登録
        UpdateLinkingDirections();
        Debug.Log("[共鳴ケーブル] リンク中");
    }

    /// <summary>
    /// アイテム：共鳴ケーブルの効果を解除する
    /// </summary>
    public void ReleasingDirection(ResonanceCableEffect effect)
    {
        if (_effects.ContainsKey(effect)) // 辞書に引数として渡されたものが登録されているか確認しておく
        {
            _effects.Remove(effect);
            UpdateLinkingDirections();
        }
    }
    
    /// <summary>
    /// 現在有効な効果から、リンクしている方向を更新する
    /// </summary>
    private void UpdateLinkingDirections()
    {
        // 一度すべての方向のリンクを解除する
        foreach (var direction in _linkingDirections)
        {
            OnRelease?.Invoke(direction);
        }
        _linkingDirections.Clear();
        
        // 残っている効果からリンクを再構築する
        foreach (var effectPair in _effects)
        {
            foreach (var direction in effectPair.Value)
            {
                if (!_linkingDirections.Contains(direction))
                {
                    _linkingDirections.Add(direction);
                    OnLink?.Invoke(direction, Color.cyan);
                }
            }
        }
        
        Debug.Log($"[共鳴ケーブル] 更新: {_linkingDirections.Count}方向がリンク中");
    }
    #endregion
}