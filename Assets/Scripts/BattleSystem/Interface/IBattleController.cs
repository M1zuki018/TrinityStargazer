using System;

/// <summary>
/// バトルコントローラー用のインターフェース
/// </summary>
public interface IBattleController
{
    bool IsVictory { get; }
    event Action OnBattleCompleated;
    event Action<DirectionEnum> OnDirectionRequest;
    void DecideEnemyDirection();
    void UseItem(ItemTypeEnum itemType, RarityEnum rarity, int count);
    void ExecuteBattle(DirectionEnum playerDirection);
    void PrepareBattleForNextTurn();
}
