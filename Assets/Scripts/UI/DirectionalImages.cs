using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// キャラクターのスプライトを管理する
/// </summary>
public class DirectionalImages : ViewBase
{
    [SerializeField] private Image _image;
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Sprite[] _directions = new Sprite[8];

    /// <summary>
    /// 引数として渡された方向のスプライトに入れ替える
    /// </summary>
    public void SetSprite(DirectionEnum direction)
    {
        if (_image == null)
        {
            _image = GetComponent<Image>();
        }
        _image.sprite = _directions[(int)direction];
    }

    /// <summary>
    /// デフォルトの画像にリセットする
    /// </summary>
    public void ResetSprite()
    {
        _image.sprite = _defaultSprite;
    }
}
