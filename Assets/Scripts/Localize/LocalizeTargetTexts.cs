using UnityEngine;

/// <summary>
/// 言語の切り替えが必要なTextコンポーネントの参照を保持しておくためのスクリプタブルオブジェクト
/// </summary>
[CreateAssetMenu(fileName = "LocalizeTargetTexts", menuName = "Scriptable Objects/LocalizeTargetTexts")]
public class LocalizeTargetTexts : ScriptableObject
{
    [SerializeField] private LocalizeTableData[] _data;

    public void Initialize()
    {
        foreach (var data in _data)
        {
            data.Target.text =
                GameManagerServiceLocator.Instance.Settings.TextLanguage == LanguageEnum.Japanese ? data.JPN : data.ENG;
        }
    }
}
