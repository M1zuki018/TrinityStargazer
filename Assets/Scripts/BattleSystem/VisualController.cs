using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// バトルの視覚的な更新を担当するクラス
/// </summary>
public class VisualController : IVisualController
{
    private readonly DirectionalImages _enemyImage;
    private readonly DirectionalImages _playerImage;
    private readonly Button[] _directionalButtons;
    private readonly TurnUIs _turnUIs;
    private readonly Color _defaultColor;
    private Tweener _forecastTweener;
    
    public VisualController(DirectionalImages enemyImage, DirectionalImages playerImage, Button[] directionalButtons, TurnUIs turnUIs)
    {
        _enemyImage = enemyImage;
        _playerImage = playerImage;
        _directionalButtons = directionalButtons;
        _turnUIs = turnUIs;
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
    /// ターン数を書き換える
    /// </summary>
    public void SetTurnText(string turnText) => _turnUIs.SetTurnText(turnText);

    /// <summary>
    /// 勝敗の表示を更新する
    /// </summary>
    public void SetResultMark(int turn, bool isVictory) => _turnUIs.SetResultMark(turn, isVictory);

    /// <summary>
    /// 指定した勝敗の表示をリセットする
    /// </summary>
    public void ResetResultMark(int turn) => _turnUIs.ResetResultMark(turn);
    
    /// <summary>
    /// 封印のページ：ボタンを使用出来ないようにする
    /// </summary>
    public void SetButtonsInteractive(DirectionEnum direction)
    {
        _directionalButtons[(int)direction].interactable = false;
    }
    
    /// <summary>
    /// 封印のページ：ボタンを使用出来るようにする
    /// </summary>
    public void SetButtonsNonInteractive(DirectionEnum direction)
    {
        _directionalButtons[(int)direction].interactable = true;
    }

    /// <summary>
    /// 共鳴ケーブル・とっておき：ボタンの色を変える
    /// </summary>
    public void ChangeButtonColor(DirectionEnum direction, Color color)
    {
        _directionalButtons[(int)direction].image.color = color;
    }

    /// <summary>
    /// 共鳴ケーブル・とっておき：ボタンの色を戻す
    /// </summary>
    public void ResetButtonColor(DirectionEnum direction)
    {
        _directionalButtons[(int)direction].image.color = _defaultColor;
    }

    /// <summary>
    /// 星の予測盤：アニメーション
    /// </summary>
    public void ForecastDirectionButton(DirectionEnum direction)
    {
        _forecastTweener?.Kill(); // 念のため、アニメーションを始める前にキルしておく
        _directionalButtons[(int)direction].transform.localScale = Vector3.one;
        _forecastTweener = _directionalButtons[(int)direction].transform.DOScale(1.2f, 1f).SetLoops(-1, LoopType.Yoyo);
    }

    /// <summary>
    /// 星の予測盤：元にもどす
    /// </summary>
    public void ReleaseForecastDirectionButton(DirectionEnum direction)
    {
        _forecastTweener?.Kill();
        _directionalButtons[(int)direction].transform.localScale = Vector3.one;
    }
}