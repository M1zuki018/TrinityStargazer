using UniRx;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// プレイヤーのレベル・名前のテキストを書き換えるクラス
/// </summary>
public class Home_PlayerDataText : MonoBehaviour
{
    private Text _text;

    private void Start()
    {
        _text = GetComponentInChildren<Text>();
        PlayerData.NameProp
            .Subscribe(_ => _text.text = $"{PlayerData.NameProp.Value} Lv. {PlayerData.LevelProp.Value}")
            .AddTo(this);
    }
}
