using System.Collections.Generic;

/// <summary>
/// バトルシステムとアイテムの効果処理の連携を担当するためのインターフェース
/// </summary>
public interface IItemEffecter
{
    void UseResonanceCable(List<DirectionEnum> directions, ResonanceCableEffect effect);
    void ReleasingDirection(ResonanceCableEffect effect);
    void UseSmartPhone(DirectionEnum direction);
    void UseReverseBroom();

    void SetGetWinPoint(int getWinPoint);
}
