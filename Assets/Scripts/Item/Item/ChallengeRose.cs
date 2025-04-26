using System;
using System.Collections.Generic;

/// <summary>
/// 「決闘の薔薇/ChallengeRose」：次の1回、相手が選んだ方向と自分が選んだ方向が一致すると必ず勝てる
/// </summary>
public class ChallengeRose : ItemBase
{
    // レアリティごとの効果をまとめた辞書
    private static Dictionary<RarityEnum, int> RarityEffects = new()
    {
        { RarityEnum.R,   1},
        { RarityEnum.SR,  2},
        { RarityEnum.SSR, 5} // 最大ターン数は変動があるので初期化時に書き換える
    };
    
    private int _getWinPoint;
    
    public ChallengeRose(RarityEnum rarity) : base(rarity, ItemTypeEnum.SealPage)
    {
        Name = "決闘の薔薇";
        EffectSetting(rarity);
        Description = $"使用したターンで勝利したとき{_getWinPoint}ポイント獲得する";
    }

    public override IItemEffect CreateEffect()
    {
        return new ChallengeRoseEffect(_getWinPoint);
    }

    /// <summary>
    /// 効果を設定する
    /// </summary>
    private void EffectSetting(RarityEnum rarity)
    {
        ModeData currentGameMode = GameManagerServiceLocator.Instance.GetGameModeData();
        RarityEffects[RarityEnum.SSR] = currentGameMode.MaxTurn;
        
        if (!RarityEffects.TryGetValue(rarity, out var effects))
        {
            throw new ArgumentException($"未知のレアリティです: {rarity}");
        }
        
        _getWinPoint = effects;
    }
}