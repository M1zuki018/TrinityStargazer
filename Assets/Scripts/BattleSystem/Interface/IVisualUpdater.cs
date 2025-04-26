/// <summary>
/// 表示を更新するインターフェース
/// </summary>
public interface IVisualUpdater
{
    void UpdateSprites(DirectionEnum enemyDirection, DirectionEnum playerDirection);
    void ResetSprites();

    public void LimitDirectionButton(DirectionEnum direction);
    public void UnlimitDirectionButton(DirectionEnum direction);
    
    public void LinkDirectionButton(DirectionEnum direction);
    public void ReleaseDirectionButton(DirectionEnum direction);
}