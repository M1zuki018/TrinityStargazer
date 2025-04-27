using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

/// <summary>
/// バトルシステムとアイテムの効果処理の連携を担当するクラス
/// </summary>
public class ItemEffecter : IItemEffecter
{
    private BattleController _controller;
    private IBattleJudge _judge;
    
    public ItemEffecter(BattleController controller, IBattleJudge judge)
    {
        _controller = controller;
        _judge = judge;
    }
    
    // アイテム：共鳴ケーブル
    public void UseResonanceCable(List<DirectionEnum> directions, ResonanceCableEffect effect)
    {
        _judge.LinkingDirection(directions, effect);
    }

    public void ReleasingDirection(ResonanceCableEffect effect)
    {
        _judge.ReleasingDirection(effect);
    }

    // アイテム：スマートフォン

    /// <summary>
    /// 自動でバトルを進行する
    /// </summary>
    public void UseSmartPhone(DirectionEnum direction)
    {
        UseSmartPhoneAsync(direction).Forget();
    }

    private async UniTask UseSmartPhoneAsync(DirectionEnum direction)
    {
        // ここで一瞬待たないと、敵の方向が決まる前にアイテム使用処理が終わってしまい、
        // 意図した挙動にならないため注意
        await UniTask.Delay(TimeSpan.FromSeconds(1)); // TODO: ここで演出
        
        _controller.RequestDirectionSelection(direction); // 方向ボタンを押したときのイベントを発火
    }
    
    // アイテム：逆行のほうき
    
    /// <summary>
    /// 1ターン巻き戻す
    /// </summary>
    public void UseReverseBroom()
    {
        _controller.RevertTurn();
    }
    
    // アイテム：決闘の薔薇
    
    /// <summary>
    /// 勝利時に獲得するポイントの数を変更する
    /// </summary>
    public void SetGetWinPoint(int getWinPoint)
    {
        _controller.SetVictoryPointValue(getWinPoint);
    }
    
}
