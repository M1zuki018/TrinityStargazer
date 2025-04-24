/// <summary>
/// 表示を更新するインターフェース
/// </summary>
public interface IVisualUpdater
{
    void UpdateSprites(DirectionEnum enemyDirection, DirectionEnum playerDirection);
    void ResetSprites();
}