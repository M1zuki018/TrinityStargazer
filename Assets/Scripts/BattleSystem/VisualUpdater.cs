using UnityEngine.UI;

/// <summary>
/// バトルの視覚的な更新を担当するクラス
/// </summary>
public class VisualUpdater : IVisualUpdater
{
    private readonly DirectionalImages _enemyImage;
    private readonly DirectionalImages _playerImage;
    private readonly Button[] _directionalButtons;
    
    public VisualUpdater(DirectionalImages enemyImage, DirectionalImages playerImage, Button[] directionalButtons)
    {
        _enemyImage = enemyImage;
        _playerImage = playerImage;
        _directionalButtons = directionalButtons;
    }
    
    /// <summary>
    /// 敵と自分のスプライトを更新する
    /// </summary>
    public void UpdateSprites(DirectionEnum enemyDirection, DirectionEnum playerDirection)
    {
        _enemyImage.SetSprite(enemyDirection);
        _playerImage.SetSprite(playerDirection);
    }
    
    /// <summary>
    /// スプライトをリセットする
    /// </summary>
    public void ResetSprites()
    {
        _enemyImage.ResetSprite();
        _playerImage.ResetSprite();
    }

    /// <summary>
    /// ボタンを使用出来ないようにする
    /// </summary>
    public void LimitDirectionButton(DirectionEnum direction)
    {
        _directionalButtons[(int)direction].interactable = false;
    }
    
    /// <summary>
    /// ボタンを使用出来るようにする
    /// </summary>
    public void UnlimitDirectionButton(DirectionEnum direction)
    {
        _directionalButtons[(int)direction].interactable = true;
    }
    
}