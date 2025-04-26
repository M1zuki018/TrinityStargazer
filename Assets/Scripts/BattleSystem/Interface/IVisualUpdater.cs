/// <summary>
/// 表示を更新するインターフェース
/// </summary>
public interface IVisualUpdater
{
    void UpdateSprites(DirectionEnum enemyDirection, DirectionEnum playerDirection);
    void ResetSprites();

    public void SetButtonsInteractive(DirectionEnum direction);
    public void SetButtonsNonInteractive(DirectionEnum direction);
    
    public void LinkDirectionButton(DirectionEnum direction);
    public void ReleaseDirectionButton(DirectionEnum direction);
    
    public void ForecastDirectionButton(DirectionEnum direction);
    public void ReleaseForecastDirectionButton(DirectionEnum direction);
}