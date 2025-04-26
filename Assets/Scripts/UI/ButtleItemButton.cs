
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// InGame中に使用するアイテム選択ボタン用のクラス
/// </summary>
public class ButtleItemButton : MonoBehaviour
{
    private ItemBase _item;
    private Button _button;
    
    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Initialize(ItemBase item)
    {
        _item = item;
        
        if (TryGetComponent(out _button))
        {
            _button.gameObject.GetComponentInChildren<Text>().text = _item.Name;
            _button.onClick.AddListener(OnClick); // メソッドを登録
        }
        
        // TODO: 色を変えたり画像を変える処理を書く
    }

    /// <summary>
    /// クリックされたときの処理
    /// </summary>
    public void OnClick()
    {
       //TODO: ここからアイテム処理を呼ぶようにしたい InventoryManager.Instance.UseItem()
    }
}
