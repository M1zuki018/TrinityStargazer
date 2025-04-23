using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// キャラクターのスプライトを管理する
/// </summary>
public class DirectionalImages : ViewBase
{
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Sprite[] _directions = new Sprite[8];
    private Image _image;

    public override UniTask OnAwake()
    {
        _image = GetComponent<Image>();
        return base.OnAwake();
    }

    /// <summary>
    /// 引数として渡された方向のスプライトに入れ替える
    /// </summary>
    public void SetSprite(DirectionEnum direction)
    {
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
