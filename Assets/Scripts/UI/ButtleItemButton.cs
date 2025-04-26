using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// InGame中に使用するアイテム選択ボタン用のクラス
/// </summary>
public class ButtleItemButton : MonoBehaviour
{
    private Button _button;
    private ItemTypeEnum _item;
    private RarityEnum _rarity;
    private CanvasController_ItemSelect _canvasController;

    private int _stock; // 在庫

    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Initialize(ItemTypeEnum itemType, RarityEnum rarity, CanvasController_ItemSelect canvasController)
    {
        if (TryGetComponent(out _button))
        {
            _item = itemType;
            _rarity = rarity;
            _canvasController = canvasController;
            _stock++;
            
            _button.gameObject.GetComponentInChildren<Text>().text = itemType.ToString();
            _button.onClick.AddListener(OnClick); // メソッドを登録
        }
        
        // TODO: 色を変えたり画像を変える処理を書く
    }

    /// <summary>
    /// クリックされたときの処理
    /// </summary>
    public void OnClick()
    {
       _canvasController.OnTestItemClick(_item, _rarity);
       _stock--; // 在庫数を減らす
       
       if (_stock <= 0)
       {
           _canvasController.RemoveList(this);
           Destroy(gameObject);
       }
    }
}
