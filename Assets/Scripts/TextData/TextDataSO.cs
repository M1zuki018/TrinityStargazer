using UnityEngine;

/// <summary>
/// ランダム会話テキストのデータを入れておくスクリプタブルオブジェクト
/// </summary>
[CreateAssetMenu(fileName = "TextData", menuName = "Scriptable Objects/TextData")]
public class TextDataSO : ScriptableObject
{
    [SerializeField] private TextData[] _textDatas;

    /// <summary>
    /// 引数で指定した文字列を取得する
    /// </summary>
    public string GetMessage(int index)
    {
        return GameManagerServiceLocator.Instance.Settings.TextLanguage == LanguageEnum.Japanese ?
            _textDatas[index].JpnText : _textDatas[index].EngText;
    }

    /// <summary>
    /// ランダムな文字列を取得する
    /// </summary>
    /// <returns></returns>
    public string GetMessage()
    {
        int rand = Random.Range(0, _textDatas.Length);
        return GameManagerServiceLocator.Instance.Settings.TextLanguage == LanguageEnum.Japanese ?
            _textDatas[rand].JpnText : _textDatas[rand].EngText; 
    }
}