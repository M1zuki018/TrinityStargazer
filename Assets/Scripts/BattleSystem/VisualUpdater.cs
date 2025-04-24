/// <summary>
/// バトルの視覚的な更新を担当するクラス
/// </summary>
public class VisualUpdater : IVisualUpdater
{
    private readonly DirectionalImages _enemyImage;
    private readonly DirectionalImages _playerImage;
    
    public VisualUpdater(DirectionalImages enemyImage, DirectionalImages playerImage)
    {
        _enemyImage = enemyImage;
        _playerImage = playerImage;
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
}