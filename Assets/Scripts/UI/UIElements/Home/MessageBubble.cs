using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 吹きだし内のセリフを管理するクラス（ボタンにつける）
/// </summary>
public class MessageBubble : WindowBase
{
    [SerializeField, ExpandableSO] private TextDataSO _textDataSO;
    private Text _text;

    private void Start()
    {
        _text = GetComponentInChildren<Text>(); // 子のテキストクラスを取得する
        base.Hide();
    }

    /// <summary>
    /// ランダムにメッセージを表示する
    /// </summary>
    public async UniTask ShowRandMessage()
    {
        base.Show();
        _text.text = _textDataSO.GetMessage();
        
        await UniTask.Delay(1000);
        
        base.Hide();
    }

    /// <summary>
    /// 指定したメッセージを表示する
    /// </summary>
    public async UniTask ShowMessage(int index)
    {
        base.Show();
        _text.text = _textDataSO.GetMessage(index);
        
        await UniTask.Delay(1000);
        
        base.Hide();
    }
}
