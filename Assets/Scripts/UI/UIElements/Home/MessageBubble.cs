using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 吹きだし内のセリフを管理するクラス
/// </summary>
[RequireComponent(typeof(Text))]
public class MessageBubble : MonoBehaviour
{
    [SerializeField, ExpandableSO] private TextDataSO _textDataSO;
    private Text _text;

    private void Start()
    {
        _text = GetComponent<Text>();
    }
    
    /// <summary>
    /// ランダムにMessageを表示する
    /// </summary>
    public void ShowRandMessage()
    {
        _text.text = _textDataSO.GetMessage();
    }
}
