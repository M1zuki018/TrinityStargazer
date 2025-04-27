/// <summary>
/// バトルシステムとアイテムの効果処理の連携を担当するためのインターフェース
/// </summary>
public interface IItemEffecter
{
    void UseSmartPhone(DirectionEnum direction);
    void UseReverseBroom();

    void SetGetWinPoint(int getWinPoint);
}
