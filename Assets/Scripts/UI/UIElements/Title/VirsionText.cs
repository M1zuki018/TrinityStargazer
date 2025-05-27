using UnityEngine.UI;

/// <summary>
/// タイトル画面のVersionのテキストを管理するクラス
/// </summary>
public class VirsionText : UIElementBase<Text>
{
    private void Start()
    {
        _element.text = $"ver {GameData.VERTION}";
    }
}
