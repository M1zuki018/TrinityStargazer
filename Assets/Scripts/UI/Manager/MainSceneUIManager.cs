using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// メインシーンのUIManager
/// </summary>
public class MainSceneUIManager : SceneUIManagerBase
{
    // 画面のインデックス定数
    private const int TITLE_SCREEN_INDEX = 0;
    private const int HOME_SCREEN_INDEX = 1;
    private const int MODESELECT_SCREEN_INDEX = 2;
    private const int SHOP_SCREEN_INDEX = 3;
    private const int ITEMMENU_SCREEN_INDEX = 4;
    private const int SETTINGS_SCREEN_INDEX = 5;
    private const int CREDIT_SCREEN_INDEX = 6;
    private const int QUIT_SCREEN_INDEX = 7;
    private const int SETTINGS_GRAPHIC_SCREEN_INDEX = 8;
    private const int SETTINGS_SOUND_SCREEN_INDEX = 9;
    private const int SETTINGS_ENVIRONMENT_SCREEN_INDEX = 10;
    private const int SETTINGS_RESET_SCREEN_INDEX = 11;

    private CanvasController_Title _ccTitle;
    private CanvasController_Home _ccHome;
    private CanvasController_ModeSelect _ccModeSelect;
    private CanvasController_Shop _ccShop;
    private CanvasController_ItemMenu _ccItemMenu;
    private CanvasController_Settings _ccSettings;
    private CanvasController_Credit _ccCredit;
    private CanvasController_Quit _ccQuit;
    private CanvasController_Settings_Graphic _ccSettingsGraphic;
    private CanvasController_Settings_Sound _ccSettingsSound;
    private CanvasController_Settings_Environment _ccSettingsEnvironment;
    private CanvasController_Settings_ResetPanel _ccSettingsResetPanel;
    
    public override UniTask OnAwake()
    {
        InitializeCanvasControllers();
        return base.OnAwake();
    }

    public override UniTask OnStart()
    {
        // 最初の読み込みではなければすぐにホーム画面に遷移する
        if (!GameManagerServiceLocator.Instance.IsFirstLoad)
        {
            _defaultCanvasIndex = HOME_SCREEN_INDEX;
        }
        return base.OnStart();
    }
    
    /// <summary>
    /// キャストしてクラスの参照を取得する
    /// </summary>
    private void InitializeCanvasControllers()
{
    try
    {
        // タイトル画面のキャンバスコントローラー
        if (_canvasObjects[TITLE_SCREEN_INDEX] is CanvasController_Title titleController)
        {
            _ccTitle = titleController;
        }
        else
        {
            Debug.LogError($"キャストに失敗しました: インデックス {TITLE_SCREEN_INDEX} のオブジェクトは CanvasController_Title ではありません");
        }

        // ホーム画面のキャンバスコントローラー
        if (_canvasObjects[HOME_SCREEN_INDEX] is CanvasController_Home homeController)
        {
            _ccHome = homeController;
        }
        else
        {
            Debug.LogError($"キャストに失敗しました: インデックス {HOME_SCREEN_INDEX} のオブジェクトは CanvasController_Home ではありません");
        }

        // モード選択画面のキャンバスコントローラー
        if (_canvasObjects[MODESELECT_SCREEN_INDEX] is CanvasController_ModeSelect modeSelectController)
        {
            _ccModeSelect = modeSelectController;
        }
        else
        {
            Debug.LogError($"キャストに失敗しました: インデックス {MODESELECT_SCREEN_INDEX} のオブジェクトは CanvasController_ModeSelect ではありません");
        }

        // ショップ画面のキャンバスコントローラー
        if (_canvasObjects[SHOP_SCREEN_INDEX] is CanvasController_Shop shopController)
        {
            _ccShop = shopController;
        }
        else
        {
            Debug.LogError($"キャストに失敗しました: インデックス {SHOP_SCREEN_INDEX} のオブジェクトは CanvasController_Shop ではありません");
        }

        // アイテムメニュー画面のキャンバスコントローラー
        if (_canvasObjects[ITEMMENU_SCREEN_INDEX] is CanvasController_ItemMenu itemMenuController)
        {
            _ccItemMenu = itemMenuController;
        }
        else
        {
            Debug.LogError($"キャストに失敗しました: インデックス {ITEMMENU_SCREEN_INDEX} のオブジェクトは CanvasController_ItemMenu ではありません");
        }

        // 設定画面のキャンバスコントローラー
        if (_canvasObjects[SETTINGS_SCREEN_INDEX] is CanvasController_Settings settingsController)
        {
            _ccSettings = settingsController;
        }
        else
        {
            Debug.LogError($"キャストに失敗しました: インデックス {SETTINGS_SCREEN_INDEX} のオブジェクトは CanvasController_Settings ではありません");
        }
        
        // クレジットパネルのキャンバスコントローラー
        if (_canvasObjects[CREDIT_SCREEN_INDEX] is CanvasController_Credit creditController)
        {
            _ccCredit = creditController;
        }
        else
        {
            Debug.LogError($"キャストに失敗しました: インデックス {CREDIT_SCREEN_INDEX} のオブジェクトは CanvasController_Credit ではありません");
        }
        
        // ゲーム終了パネルのキャンバスコントローラー
        if (_canvasObjects[QUIT_SCREEN_INDEX] is CanvasController_Quit quitController)
        {
            _ccQuit = quitController;
        }
        else
        {
            Debug.LogError($"キャストに失敗しました: インデックス {QUIT_SCREEN_INDEX} のオブジェクトは CanvasController_Quit ではありません");
        }
        
        // 設定画面・グラフィック設定のキャンバスコントローラー
        if (_canvasObjects[SETTINGS_GRAPHIC_SCREEN_INDEX] is CanvasController_Settings_Graphic settingsGraphicController)
        {
            _ccSettingsGraphic = settingsGraphicController;
        }
        else
        {
            Debug.LogError($"キャストに失敗しました: インデックス {SETTINGS_GRAPHIC_SCREEN_INDEX} のオブジェクトは CanvasController_Settings_Graphic ではありません");
        }
        
        // 設定画面・グラフィック設定のキャンバスコントローラー
        if (_canvasObjects[SETTINGS_SOUND_SCREEN_INDEX] is CanvasController_Settings_Sound settingsSoundController)
        {
            _ccSettingsSound = settingsSoundController;
        }
        else
        {
            Debug.LogError($"キャストに失敗しました: インデックス {SETTINGS_SOUND_SCREEN_INDEX} のオブジェクトは CanvasController_Settings_Sound ではありません");
        }
        
        // 設定画面・環境設定のキャンバスコントローラー
        if (_canvasObjects[SETTINGS_ENVIRONMENT_SCREEN_INDEX] is CanvasController_Settings_Environment settingsEnvironmentController)
        {
            _ccSettingsEnvironment = settingsEnvironmentController;
        }
        else
        {
            Debug.LogError($"キャストに失敗しました: インデックス {SETTINGS_ENVIRONMENT_SCREEN_INDEX} のオブジェクトは CanvasController_Settings_Environment ではありません");
        }
        
        // 設定画面・リセット設定のキャンバスコントローラー
        if (_canvasObjects[SETTINGS_RESET_SCREEN_INDEX] is CanvasController_Settings_ResetPanel settingsResetPanelController)
        {
            _ccSettingsResetPanel = settingsResetPanelController;
        }
        else
        {
            Debug.LogError($"キャストに失敗しました: インデックス {SETTINGS_RESET_SCREEN_INDEX} のオブジェクトは CanvasController_Settings_ResetPanel ではありません");
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
        if (_ccTitle != null)
        {
            _ccTitle.OnHomeButtonClicked += HandleToHome;
            _ccTitle.OnCreditButtonClicked += HandleOpenCredit;
            _ccTitle.OnQuitButtonClicked += HandleOpenQuit;
        }

        if (_ccHome != null)
        {
            _ccHome.OnModeSelectButtonClicked += HandleToModeSelect;
            _ccHome.OnShopButtonClicked += HandleToShop;
            _ccHome.OnItemMenuButtonClicked += HandleToItemMenu;
            _ccHome.OnSettingsButtonClicked += HandleToSettings;
        }

        if (_ccModeSelect != null)
        {
            _ccModeSelect.OnHomeButtonClicked += HandleToHome;
            _ccModeSelect.OnGameModeButtonClicked += HandleToInGame;
        }

        if (_ccShop != null)
        {
            _ccShop.OnHomeButtonClicked += HandleToHome;
        }

        if (_ccItemMenu != null)
        {
            _ccItemMenu.OnHomeButtonClicked += HandleToHome;
        }
        
        if (_ccSettings != null)
        {
            _ccSettings.OnHomeButtonClicked += HandleToHome;
            _ccSettings.OnGraphicButtonClicked += HandleOpenGraphicSettings;
            _ccSettings.OnSoundButtonClicked += HandleOpenSoundSettings;
            _ccSettings.OnEnvironmentButtonClicked += HandleOpenEnvironmentSettings;
            _ccSettings.OnResetButtonClicked += HandleOpenResetPanel;
        }

        if (_ccCredit != null)
        {
            _ccCredit.OnCloseButtonClicked += HandlePopCanvas;
        }

        if (_ccQuit != null)
        {
            _ccQuit.OnNoButtonClicked += HandlePopCanvas;
        }

        if (_ccSettingsGraphic != null)
        {
            
        }

        if (_ccSettingsEnvironment != null)
        {
            
        }

        if (_ccSettingsSound != null)
        {
            
        }

        if (_ccSettingsResetPanel != null)
        {
            _ccSettingsResetPanel.OnButtonClicked += HandleToSettings;
        }
    }

    #region 各遷移用メソッド

    /// <summary>
    /// オーバーレイを一つ閉じる
    /// </summary>
    private void HandlePopCanvas() => PopCanvas();

    /// <summary>
    /// タイトル画面へ遷移
    /// </summary>
    private void HandleToTitle() => ShowCanvas(TITLE_SCREEN_INDEX);

    /// <summary>
    /// ホーム画面へ遷移
    /// </summary>
    private void HandleToHome() => ShowCanvas(HOME_SCREEN_INDEX);
    
    /// <summary>
    /// モード選択画面へ遷移
    /// </summary>
    private void HandleToModeSelect() => ShowCanvas(MODESELECT_SCREEN_INDEX);
    
    /// <summary>
    /// ショップ画面へ遷移
    /// </summary>
    private void HandleToShop() => ShowCanvas(SHOP_SCREEN_INDEX);
    
    /// <summary>
    /// アイテムメニュー画面へ遷移
    /// </summary>
    private void HandleToItemMenu() => ShowCanvas(ITEMMENU_SCREEN_INDEX);
    
    /// <summary>
    /// 設定画面へ遷移
    /// </summary>
    private void HandleToSettings() => ShowCanvas(SETTINGS_SCREEN_INDEX);
    
    /// <summary>
    /// インゲームへ遷移
    /// </summary>
    private void HandleToInGame() => SceneManager.LoadScene(1);
    
    /// <summary>
    /// クレジットパネルを開く
    /// </summary>
    private void HandleOpenCredit() => PushCanvas(CREDIT_SCREEN_INDEX);
    
    /// <summary>
    /// ゲーム終了パネルを開く
    /// </summary>
    private void HandleOpenQuit() => PushCanvas(QUIT_SCREEN_INDEX);

    /// <summary>
    /// グラフィック設定画面を開く
    /// </summary>
    private void HandleOpenGraphicSettings()
    {
        PushCanvas(SETTINGS_GRAPHIC_SCREEN_INDEX);
        Debug.Log("あ");
    }

    /// <summary>
    /// サウンド設定を開く
    /// </summary>
    private void HandleOpenSoundSettings() => PushCanvas(SETTINGS_SOUND_SCREEN_INDEX);
    
    /// <summary>
    /// 環境設定を開く
    /// </summary>
    private void HandleOpenEnvironmentSettings() => PushCanvas(SETTINGS_ENVIRONMENT_SCREEN_INDEX);
    
    /// <summary>
    /// データリセットパネルを開く
    /// </summary>
    private void HandleOpenResetPanel() => PushCanvas(SETTINGS_RESET_SCREEN_INDEX);
    
    #endregion
    

    private void OnDestroy()
    {
        if (_ccTitle != null)
        {
            _ccTitle.OnHomeButtonClicked -= HandleToHome;
            _ccTitle.OnCreditButtonClicked -= HandleOpenCredit;
            _ccTitle.OnQuitButtonClicked -= HandleOpenQuit;
        }

        if (_ccHome != null)
        {
            _ccHome.OnModeSelectButtonClicked -= HandleToModeSelect;
            _ccHome.OnShopButtonClicked -= HandleToShop;
            _ccHome.OnItemMenuButtonClicked -= HandleToItemMenu;
            _ccHome.OnSettingsButtonClicked -= HandleToSettings;
        }

        if (_ccModeSelect != null)
        {
            _ccModeSelect.OnHomeButtonClicked -= HandleToHome;
            _ccModeSelect.OnGameModeButtonClicked -= HandleToInGame;
        }

        if (_ccShop != null)
        {
            _ccShop.OnHomeButtonClicked -= HandleToHome;
        }

        if (_ccItemMenu != null)
        {
            _ccItemMenu.OnHomeButtonClicked -= HandleToHome;
        }

        if (_ccSettings != null)
        {
            _ccSettings.OnHomeButtonClicked -= HandleToHome;
            _ccSettings.OnGraphicButtonClicked -= HandleOpenGraphicSettings;
            _ccSettings.OnSoundButtonClicked -= HandleOpenSoundSettings;
            _ccSettings.OnEnvironmentButtonClicked -= HandleOpenEnvironmentSettings;
            _ccSettings.OnResetButtonClicked -= HandleOpenResetPanel;
        }

        if (_ccCredit != null)
        {
            _ccCredit.OnCloseButtonClicked -= HandlePopCanvas;
        }

        if (_ccQuit != null)
        {
            _ccQuit.OnNoButtonClicked -= HandlePopCanvas;
        }
        
        
        
        if (_ccSettingsResetPanel != null)
        {
            _ccSettingsResetPanel.OnButtonClicked -= HandleToSettings;
        }
    }
}
