using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// タイトル画面のVersionのテキストを管理するクラス
/// </summary>
public class VirsionText : MonoBehaviour
{
    private Text _text;

    private void Start()
    {
        _text = GetComponent<Text>();
        _text.text = $"Version. {GameData.VERSTION}";
    }
}
