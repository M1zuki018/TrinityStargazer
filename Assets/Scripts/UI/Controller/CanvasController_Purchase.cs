using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ショップの購入画面のキャンバスコントローラー
/// </summary>
public class CanvasController_Purchase : WindowBase
{
    [SerializeField] private Transform _itemButtonsParent; // アイテムのボタンを入れるエリア
    [SerializeField] private Text _explainText; // アイテムの説明
    [SerializeField] private GameObject _itemButtonPrefab; // アイテムのボタンのPrefab
    [SerializeField] private Image _itemImageArea; // アイテムの画像を表示するエリア
    private List<Button> _currentButtons = new List<Button>();
    
    public override UniTask OnUIInitialize()
    {
        // アイテムのボタン
        CreateItemButtons(6);
        return base.OnUIInitialize();
    }

    /// <summary>
    /// アイテムのボタンを生成する
    /// </summary>
    private void CreateItemButtons(int itemCount)
    {
        for (int i = 0; i < itemCount; i++)
        {
            // ボタンを指定の親オブジェクトの子に表示した上でButtonコンポーネントを取得
            Button itemButton = Instantiate(_itemButtonPrefab, _itemButtonsParent).GetComponent<Button>();
            _currentButtons.Add(itemButton);
        }
    }
}
