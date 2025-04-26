using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// バトルの視覚的な更新を担当するクラス
/// </summary>
public class VisualUpdater : IVisualUpdater
{
    private readonly DirectionalImages _enemyImage;
    private readonly DirectionalImages _playerImage;
    private readonly Button[] _directionalButtons;
    private readonly Color _defaultColor;
    
    public VisualUpdater(DirectionalImages enemyImage, DirectionalImages playerImage, Button[] directionalButtons)
    {
        _enemyImage = enemyImage;
        _playerImage = playerImage;
        _directionalButtons = directionalButtons;
        _defaultColor = _directionalButtons[0].image.color;
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
    /// 封印のページ：ボタンを使用出来ないようにする
    /// </summary>
    public void LimitDirectionButton(DirectionEnum direction)
    {
        _directionalButtons[(int)direction].interactable = false;
    }
    
    /// <summary>
    /// 封印のページ：ボタンを使用出来るようにする
    /// </summary>
    public void UnlimitDirectionButton(DirectionEnum direction)
    {
        _directionalButtons[(int)direction].interactable = true;
    }

    /// <summary>
    /// 共鳴ケーブル：ボタンの色を変える
    /// </summary>
    public void LinkDirectionButton(DirectionEnum direction)
    {
        _directionalButtons[(int)direction].image.color = Color.cyan;
    }

    /// <summary>
    /// 共鳴ケーブル：ボタンの色を戻す
    /// </summary>
    public void ReleaseDirectionButton(DirectionEnum direction)
    {
        _directionalButtons[(int)direction].image.color = _defaultColor;
    }
}