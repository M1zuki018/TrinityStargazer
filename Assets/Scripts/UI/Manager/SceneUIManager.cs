using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// シーンごとに配置されるUIManager
/// </summary>
public class SceneUIManager : ViewBase
{
    [SerializeField] private List<WindowBase> _canvasObjects = new List<WindowBase>();
    [SerializeField] private int _defaultCanvasIndex = 0;
    
    private int _currentCanvasIndex = -1; // 現在表示中のキャンバスインデックス
    public event Action<int, int> OnBeforeCanvasChange; // キャンバス切り替え前のイベント(前のインデックス, 次のインデックス)
    public event Action<int> OnAfterCanvasChange;     // キャンバス切り替え後のイベント(現在のインデックス)

    public override UniTask OnStart()
    {
        ShowCanvas(_defaultCanvasIndex);
        return base.OnStart();
    }

    /// <summary>
    /// 指定したキャンバスを表示し、それ以外を非表示にする
    /// </summary>
    public void ShowCanvas(int index)
    {
        // インデックスの範囲チェック
        if (index < 0 || index >= _canvasObjects.Count)
        {
            Debug.LogError($"キャンバスインデックスが範囲外です: {index}");
            return;
        }
        
        // 同じキャンバスを表示しようとしている場合は何もしない
        if (_currentCanvasIndex == index)
            return;
        
        OnBeforeCanvasChange?.Invoke(_currentCanvasIndex, index); // 切り替え前イベント発火
        
        // キャンバス切り替え
        for (int i = 0; i < _canvasObjects.Count; i++)
        {
            if (i == index)
            {
                _canvasObjects[i]?.Show();
            }
            else
            {
                _canvasObjects[i]?.Hide();
            }
        }
        
        _currentCanvasIndex = index; // 現在のインデックスを更新
        OnAfterCanvasChange?.Invoke(_currentCanvasIndex); // 切り替え後イベント発火
    }
    
    /// <summary>
    /// 現在のキャンバスインデックスを取得
    /// </summary>
    public int GetCurrentCanvasIndex()
    {
        return _currentCanvasIndex;
    }
}