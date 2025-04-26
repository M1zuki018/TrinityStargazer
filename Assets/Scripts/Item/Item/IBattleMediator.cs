using System;
using System.Collections.Generic;

/// <summary>
/// アイテム効果とバトルシステムを繋ぐ仲介用インターフェース
/// </summary>
public interface IBattleMediator
{
    IDirectionDecider DirectionDecider { get; }
    IBattleJudge BattleJudge { get; }
    IVisualUpdater VisualUpdater { get; }
    // 他の必要なコンポーネント...
    
    void RegisterEffect(IItemEffect effect);
    void RemoveEffect(IItemEffect effect);
    void UpdateEffects(); // ターン更新時など
}