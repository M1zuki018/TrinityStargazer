using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// バトルシステムとアイテムの効果処理の連携を担当するためのインターフェース
/// </summary>
public interface IItemEffecter
{
    void SetButtonsInteractive(DirectionEnum direction);
    void SetButtonsNonInteractive(DirectionEnum direction);
    
    void ChangeButtonColor(DirectionEnum direction, Color color);
    void ResetButtonColor(DirectionEnum direction);
    
    // 星の予測盤
    void ForecastDirectionButton(DirectionEnum direction);
    void ReleaseForecastDirectionButton(DirectionEnum direction);
    
    // 共鳴ケーブル
    void UseResonanceCable(List<DirectionEnum> directions, ResonanceCableEffect effect);
    void ReleasingDirection(ResonanceCableEffect effect);
    
    // スマートフォン
    void UseSmartPhone(DirectionEnum direction);
    
    // 逆行のほうき
    void UseReverseBroom();

    // 決闘の薔薇
    void SetGetWinPoint(int getWinPoint);
    
    HashSet<DirectionEnum> GetSealedDirections();
    void ModifyProbability(DirectionEnum direction, float addedProbability);
    void LimitProbability(DirectionEnum direction);
    void RemoveLimitProbability(DirectionEnum direction);
    void ResetProbabilities();
    
    event Action<DirectionEnum> OnEnemyDirectionChanged;
}
