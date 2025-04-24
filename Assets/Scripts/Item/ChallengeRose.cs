using UnityEngine;

/// <summary>
/// 「決闘の薔薇/ChallengeRose」：次の1回、相手が選んだ方向と自分が選んだ方向が一致すると必ず勝てる
/// </summary>
public class ChallengeRose : ItemBase
{
    public ChallengeRose(RarityEnum rarity)
    {
        Name = "";
        Rarity = rarity;
    }
    
    public override void Use()
    {
        throw new System.NotImplementedException();
    }
}