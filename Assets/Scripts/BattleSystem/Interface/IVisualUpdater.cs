using UnityEngine;

/// <summary>
/// 表示を更新するインターフェース
/// </summary>
public interface IVisualUpdater
{
    void UpdateSprites(DirectionEnum enemyDirection, DirectionEnum playerDirection);
    void ResetSprites();

    public void SetButtonsInteractive(DirectionEnum direction);
    public void SetButtonsNonInteractive(DirectionEnum direction);
    
    public void ChangeButtonColor(DirectionEnum direction, Color color);
    public void ResetButtonColor(DirectionEnum direction);
    
    public void ForecastDirectionButton(DirectionEnum direction);
    public void ReleaseForecastDirectionButton(DirectionEnum direction);
}