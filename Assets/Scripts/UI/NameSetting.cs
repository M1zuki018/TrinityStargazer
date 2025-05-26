using UnityEngine;
using UnityEngine.UI;

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
            _inputField.text = $"{PlayerData.Name}"; // プレイヤー名を入れておく
            _inputField.onEndEdit.AddListener(ChangeName);
        }
    }

    /// <summary>
    /// プレイヤー名を変更する
    /// </summary>
    private void ChangeName(string newName) => PlayerData.SetName(newName);

    private void OnDestroy()
    {
        if(_inputField != null) _inputField.onEndEdit.RemoveAllListeners();
    }
    
}
