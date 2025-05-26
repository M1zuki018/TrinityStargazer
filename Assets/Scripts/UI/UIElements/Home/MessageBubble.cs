using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 吹きだし内のセリフを管理するクラス（ボタンにつける）
/// </summary>
public class MessageBubble : WindowBase
{
    [Header("初期設定")]
    [SerializeField] private float _characterDelay = 0.05f; // 一文字あたりの表示にかける時間
    [SerializeField] private float _displayDuration = 2f; // 文字が表示されきってから吹きだしが非表示になるまでの時間
    
    [Header("テキストデータ")]
    [SerializeField, ExpandableSO] private TextDataSO _textDataSO;
    
    private Text _text;
    private Sequence _sequence;

    private void Start()
    {
        _text = GetComponentInChildren<Text>(); // 子のテキストクラスを取得する
        base.Hide();
    }

    /// <summary>
    /// ランダムにメッセージを表示する
    /// </summary>
    public void ShowRandMessage() => UpdateText(_textDataSO.GetMessage());
    
    /// <summary>
    /// インデックスで指定したメッセージを表示する
    /// </summary>
    public void ShowRandMessage(int index) => UpdateText(_textDataSO.GetMessage(index));

    /// <summary>
    /// テキストの更新処理
    /// </summary>
    private void UpdateText(string message)
    {
        _sequence?.Kill(); // 再生中のシーケンスがあればKillする
        
        _text.text = ""; // 一旦テキストを空にする
        base.Show();
        
        _sequence = DOTween.Sequence(); // シーケンスを初期化
        _sequence.Append(_text.DOText(message, message.Length * _characterDelay).SetEase(Ease.Linear));
        _sequence.AppendInterval(_displayDuration); // 待機
        
        _sequence.OnComplete(() => base.Hide());
    }
}
