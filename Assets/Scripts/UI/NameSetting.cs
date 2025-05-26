using UnityEngine;
using UnityEngine.UI;
using UniRx;

/// <summary>
/// ホーム画面の名前を設定するボタンのクラス
/// </summary>
public class NameSetting : MonoBehaviour
{
    private InputField _inputField; // プレイヤー名を入力するクラス

    private void Start()
    {
        _inputField = gameObject.GetComponentInChildren<InputField>(); // 子オブジェクトから取得

        if (_inputField != null)
        {
            _inputField.onEndEdit.AddListener(ChangeName);
        }
        
        PlayerData.NameProp
            .Subscribe(name => _inputField.text = name)
            .AddTo(this);
    }

    /// <summary>
    /// プレイヤー名を変更する
    /// </summary>
    private void ChangeName(string newName) => PlayerData.NameProp.Value = newName;

    private void OnDestroy()
    {
        if(_inputField != null) _inputField.onEndEdit.RemoveAllListeners();
    }
    
}
