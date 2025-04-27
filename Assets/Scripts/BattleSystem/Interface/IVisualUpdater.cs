using UnityEngine;

/// <summary>
/// 表示を更新するインターフェース
/// </summary>
public interface IVisualUpdater
{
    // 相手の顔の向き・自分の手の向きを変更する
    void UpdateSprites(DirectionEnum enemyDirection, DirectionEnum playerDirection);
    void ResetSprites();

    // ターンの表示に関するメソッド
    void SetTurnText(string turnText);
    void SetResultMark(int turn, bool isVictory);
    void ResetResultMark(int turn);

    void SetButtonsInteractive(DirectionEnum direction);
    void SetButtonsNonInteractive(DirectionEnum direction);
    
    void ChangeButtonColor(DirectionEnum direction, Color color);
    void ResetButtonColor(DirectionEnum direction);
    
    void ForecastDirectionButton(DirectionEnum direction);
    void ReleaseForecastDirectionButton(DirectionEnum direction);
}