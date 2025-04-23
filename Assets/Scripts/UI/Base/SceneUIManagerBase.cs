using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

/// <summary>
/// UIマネージャーの基底クラス
/// </summary>
public abstract class SceneUIManagerBase : ViewBase
{
    [SerializeField] protected List<WindowBase> _canvasObjects = new List<WindowBase>();
    [SerializeField] protected int _defaultCanvasIndex = 0;
    
    protected int _currentCanvasIndex = -1; // 現在表示中のキャンバスインデックス
    public event Action<int, int> OnBeforeCanvasChange; // キャンバス切り替え前のイベント(前のインデックス, 次のインデックス)
    public event Action<int> OnAfterCanvasChange;     // キャンバス切り替え後のイベント(現在のインデックス)

    public override UniTask OnBind()
    {
        RegisterWindowEvents();
        return base.OnBind();
    }
    
    public override UniTask OnStart()
    {
        ShowCanvas(_defaultCanvasIndex);
        return base.OnStart();
    }

    /// <summary>
    /// 指定したキャンバスを表示し、それ以外を非表示にする
    /// </summary>
    public virtual void ShowCanvas(int index)
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
        GameManager.Instance.ChangeGameState((GameStateEnum)index); // ゲームの状態を更新
        OnAfterCanvasChange?.Invoke(_currentCanvasIndex); // 切り替え後イベント発火
    }
    
    /// <summary>
    /// 指定したキャンバスを表示し、それ以外を非表示にする
    /// </summary>
    public virtual void ShowAndBlockCanvas(int index)
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
                _canvasObjects[i]?.Block();
            }
        }
        
        _currentCanvasIndex = index; // 現在のインデックスを更新
        GameManager.Instance.ChangeGameState((GameStateEnum)index); // ゲームの状態を更新
        OnAfterCanvasChange?.Invoke(_currentCanvasIndex); // 切り替え後イベント発火
    }

    /// <summary>
    /// 指定したキャンバスを表示し、それ以外を非表示にする
    /// </summary>
    public virtual void CloseAndUnBlockCanvas(int index)
    {
        // インデックスの範囲チェック
        if (index < 0 || index >= _canvasObjects.Count)
        {
            Debug.LogError($"キャンバスインデックスが範囲外です: {index}");
            return;
        }
        
        // 一番上に表示されているキャンバス以外を操作しようとした場合は何もしない
        if (_currentCanvasIndex != index)
             return;
        
        OnBeforeCanvasChange?.Invoke(_currentCanvasIndex, index); // 切り替え前イベント発火
        
        // キャンバス切り替え
        for (int i = 0; i < _canvasObjects.Count; i++)
        {
            if (i == index)
            {
                _canvasObjects[i]?.Hide();
            }
            else
            {
                _canvasObjects[i]?.Unblock();
            }
        }
        
        _currentCanvasIndex = index; // 現在のインデックスを更新
        //TODO: GameManager.Instance.ChangeGameState((GameStateEnum)index); // ゲームの状態を更新
        OnAfterCanvasChange?.Invoke(_currentCanvasIndex); // 切り替え後イベント発火
    }
    
    /// <summary>
    /// 現在のキャンバスインデックスを取得
    /// </summary>
    public int GetCurrentCanvasIndex()
    {
        return _currentCanvasIndex;
    }
    
    /// <summary>
    /// 画面遷移イベントの登録を行う
    /// </summary>
    protected abstract void RegisterWindowEvents();
}