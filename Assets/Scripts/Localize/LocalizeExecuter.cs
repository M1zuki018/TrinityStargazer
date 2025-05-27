using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ローカライズを実行するクラス
/// </summary>
public class LocalizeExecuter : ViewBase
{
    [SerializeField] private Text[] _targetText;
    [SerializeField, ExpandableSO] private LocalizeTargetTexts _datas;

    private void OnEnable()
    {
        if(GameManagerServiceLocator.Instance == null) return; // GameManagerのインスタンスが生成される前であれば処理は行わない
        Execute();
    }
    
    public override UniTask OnUIInitialize()
    {
        Execute();
        return base.OnUIInitialize();
    }

    private void Execute()
    {
        Debug.Log("LocalizeExecuter");
        for(int i = 0; i < _datas.Data.Length; i++)
        {
            _targetText[i].text =
                GameManagerServiceLocator.Instance.Settings.TextLanguage == LanguageEnum.Japanese ? _datas.Data[i].JPN : _datas.Data[i].ENG;
        }
    }
}
