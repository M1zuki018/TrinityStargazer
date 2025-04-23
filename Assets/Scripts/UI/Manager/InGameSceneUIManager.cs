using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// インゲームシーンのUIManager
/// </summary>
public class InGameSceneUIManager : SceneUIManagerBase
{
    // 操作状態のenumを変更するときにindexがずれるためこの定数を使う
    private const int MAIN_SCREEN_CANVAS = 6;
    
    // 画面のインデックス定数
    private const int BASE_SCREEN_INDEX = 0;
    private const int BEFORE_SCREEN_INDEX = 1;
    private const int ITEMSELECT_SCREEN_INDEX = 2;
    private const int CHAT_SCREEN_INDEX = 3;
    private const int PAUSE_SCREEN_INDEX = 4;
    private const int DIRECTION_SCREEN_INDEX = 5;
    private const int AFTER_SCREEN_INDEX = 6;
    private const int RESULT_SCREEN_INDEX = 7;

    private CanvasController_Base _ccBase;
    private CanvasController_Before _ccBefore;
    private CanvasController_ItemSelect _ccItemSelect;
    private CanvasController_Chat _ccChat;
    private CanvasController_Pause _ccPause;
    private CanvasController_Direction _ccDirection;
    private CanvasController_After _ccAfter;
    private CanvasController_Result _ccResult;
    
    public override UniTask OnAwake()
    {
        InitializeCanvasControllers();
        return base.OnAwake();
    }

    /// <summary>
    /// キャストしてクラスの参照を取得する
    /// </summary>
    private void InitializeCanvasControllers()
    {
        try
        {
            // ベース画面のキャンバスコントローラー
            if (_canvasObjects[BASE_SCREEN_INDEX] is CanvasController_Base baseController)
            {
                _ccBase = baseController;
            }
            else
            {
                Debug.LogError($"キャストに失敗しました: インデックス {BASE_SCREEN_INDEX} のオブジェクトは CanvasController_Base ではありません");
            }

            // Before画面のキャンバスコントローラー
            if (_canvasObjects[BEFORE_SCREEN_INDEX] is CanvasController_Before beforeController)
            {
                _ccBefore = beforeController;
            }
            else
            {
                Debug.LogError($"キャストに失敗しました: インデックス {BEFORE_SCREEN_INDEX} のオブジェクトは CanvasController_Before ではありません");
            }

            // アイテム選択画面のキャンバスコントローラー
            if (_canvasObjects[ITEMSELECT_SCREEN_INDEX] is CanvasController_ItemSelect itemSelectController)
            {
                _ccItemSelect = itemSelectController;
            }
            else
            {
                Debug.LogError(
                    $"キャストに失敗しました: インデックス {ITEMSELECT_SCREEN_INDEX} のオブジェクトは CanvasController_ItemSelect ではありません");
            }

            // チャット画面のキャンバスコントローラー
            if (_canvasObjects[CHAT_SCREEN_INDEX] is CanvasController_Chat chatController)
            {
                _ccChat = chatController;
            }
            else
            {
                Debug.LogError($"キャストに失敗しました: インデックス {CHAT_SCREEN_INDEX} のオブジェクトは CanvasController_Chat ではありません");
            }

            // ポーズ画面のキャンバスコントローラー
            if (_canvasObjects[PAUSE_SCREEN_INDEX] is CanvasController_Pause pauseController)
            {
                _ccPause = pauseController;
            }
            else
            {
                Debug.LogError($"キャストに失敗しました: インデックス {PAUSE_SCREEN_INDEX} のオブジェクトは CanvasController_Pause ではありません");
            }

            // 演出画面のキャンバスコントローラー
            if (_canvasObjects[DIRECTION_SCREEN_INDEX] is CanvasController_Direction directionController)
            {
                _ccDirection = directionController;
            }
            else
            {
                Debug.LogError(
                    $"キャストに失敗しました: インデックス {DIRECTION_SCREEN_INDEX} のオブジェクトは CanvasController_Direction ではありません");
            }

            // After画面のキャンバスコントローラー
            if (_canvasObjects[AFTER_SCREEN_INDEX] is CanvasController_After afterController)
            {
                _ccAfter = afterController;
            }
            else
            {
                Debug.LogError($"キャストに失敗しました: インデックス {AFTER_SCREEN_INDEX} のオブジェクトは CanvasController_After ではありません");
            }

            // 結果画面のキャンバスコントローラー
            if (_canvasObjects[RESULT_SCREEN_INDEX] is CanvasController_Result resultController)
            {
                _ccResult = resultController;
            }
            else
            {
                Debug.LogError($"キャストに失敗しました: インデックス {RESULT_SCREEN_INDEX} のオブジェクトは CanvasController_Result ではありません");
            }
        }
        catch (IndexOutOfRangeException ex)
        {
            Debug.LogError($"配列インデックスが範囲外です: {ex.Message}");
        }
        catch (Exception ex)
        {
            Debug.LogError($"キャンバスコントローラーの初期化中にエラーが発生しました: {ex.Message}");
        }
    }

    protected override void RegisterWindowEvents()
    {
        if (_ccBase != null)
        {
            _ccBase.OnPauseButtonClicked += HandlePause;
        }
        
        if (_ccBefore != null)
        {
            _ccBefore.OnBattleButtonClicked += HandleBattle;
            _ccBefore.OnItemSelectButtonClicked += HandleItemSelect;
            _ccBefore.OnChatButtonClicked += HandleChat;
        }

        if (_ccPause != null)
        {
            _ccPause.OnResumeButtonClicked += HandleResume;
            _ccPause.OnQuitButtonClicked += HandleToTitle;
        }
    }

    /// <summary>
    /// ホームのシーンへ遷移
    /// </summary>
    private void HandleToTitle() => SceneManager.LoadScene(0);

    /// <summary>
    /// ポーズ画面を開く
    /// </summary>
    private void HandlePause() => ShowAndBlockCanvas(PAUSE_SCREEN_INDEX);

    /// <summary>
    /// バトル画面へ遷移
    /// </summary>
    private void HandleBattle() => ShowAndBlockCanvas(BASE_SCREEN_INDEX);

    /// <summary>
    /// アイテム選択画面へ遷移
    /// </summary>
    private void HandleItemSelect() => ShowAndBlockCanvas(ITEMSELECT_SCREEN_INDEX);

    /// <summary>
    /// チャット画面へ遷移
    /// </summary>
    private void HandleChat() => ShowAndBlockCanvas(CHAT_SCREEN_INDEX);
    
    /// <summary>
    /// ポーズ画面を閉じる
    /// </summary>
    private void HandleResume() => CloseAndUnBlockCanvas(PAUSE_SCREEN_INDEX);

    private void OnDestroy()
    {
        if (_ccBase != null)
        {
            _ccBase.OnPauseButtonClicked -= HandlePause;
        }
        
        if (_ccBefore != null)
        {
            _ccBefore.OnBattleButtonClicked -= HandleBattle;
            _ccBefore.OnItemSelectButtonClicked -= HandleItemSelect;
            _ccBefore.OnChatButtonClicked -= HandleChat;
        }

        if (_ccPause != null)
        {
            _ccPause.OnResumeButtonClicked -= HandleResume;
            _ccPause.OnQuitButtonClicked -= HandleToTitle;
        }
    }
}
