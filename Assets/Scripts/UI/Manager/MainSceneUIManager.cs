using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// メインシーンのUIManager
/// </summary>
public class MainSceneUIManager : SceneUIManagerBase
{
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
    private CanvasController_PlayerData _ccPlayerData;
    
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
            _defaultCanvasIndex = (int)InGameCanvasEnum.Home;
        }
        return base.OnStart();
    }
    
    /// <summary>
    /// キャストのためのメソッド
    /// </summary>
    private T InitializeController<T>(int index) where T : class
    {
        try
        {
            if (_canvasObjects[index] is T controller)
            {
                return controller;
            }
            Debug.LogError($"キャストに失敗しました: インデックス {index} のオブジェクトは {typeof(T)} ではありません");
        }
        catch (Exception ex)
        {
            Debug.LogError($"{typeof(T)}の初期化中にエラーが発生しました: {ex.Message}");
        }
        return null;
    }

    /// <summary>
    /// キャストしてクラスの参照を取得する
    /// </summary>
    private void InitializeCanvasControllers()
    {
        _ccTitle = InitializeController<CanvasController_Title>((int)InGameCanvasEnum.Title);
        _ccHome = InitializeController<CanvasController_Home>((int)InGameCanvasEnum.Home);
        _ccModeSelect = InitializeController<CanvasController_ModeSelect>((int)InGameCanvasEnum.ModeSelect);
        _ccShop = InitializeController<CanvasController_Shop>((int)InGameCanvasEnum.Shop);
        _ccItemMenu = InitializeController<CanvasController_ItemMenu>((int)InGameCanvasEnum.ItemMenu);
        _ccSettings = InitializeController<CanvasController_Settings>((int)InGameCanvasEnum.Settings);
        _ccCredit = InitializeController<CanvasController_Credit>((int)InGameCanvasEnum.Credit);
        _ccQuit = InitializeController<CanvasController_Quit>((int)InGameCanvasEnum.Quit);
        _ccSettingsGraphic = InitializeController<CanvasController_Settings_Graphic>((int)InGameCanvasEnum.SettingsGraphic);
        _ccSettingsSound = InitializeController<CanvasController_Settings_Sound>((int)InGameCanvasEnum.SettingsSound);
        _ccSettingsEnvironment = InitializeController<CanvasController_Settings_Environment>((int)InGameCanvasEnum.SettingsEnvironment);
        _ccSettingsResetPanel = InitializeController<CanvasController_Settings_ResetPanel>((int)InGameCanvasEnum.SettingsReset);
        _ccPlayerData = InitializeController<CanvasController_PlayerData>((int)InGameCanvasEnum.PlayerData);
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
            _ccHome.OnPlayerDataButtonClicked += HandleOpenPlayerData;
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
            _ccSettingsGraphic.OnCloseButtonClicked += HandleToSettings;
        }

        if (_ccSettingsEnvironment != null)
        {
            _ccSettingsEnvironment.OnCloseButtonClicked += HandleToSettings;
        }

        if (_ccSettingsSound != null)
        {
            _ccSettingsSound.OnCloseButtonClicked += HandleToSettings;
        }

        if (_ccSettingsResetPanel != null)
        {
            _ccSettingsResetPanel.OnButtonClicked += HandleToSettings;
        }

        if (_ccPlayerData != null)
        {
            _ccPlayerData.OnCloseButtonClicked += HandlePopCanvas;
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
    private void HandleToTitle() => ShowCanvas((int)InGameCanvasEnum.Title);

    /// <summary>
    /// ホーム画面へ遷移
    /// </summary>
    private void HandleToHome() => ShowCanvas((int)InGameCanvasEnum.Home);
    
    /// <summary>
    /// モード選択画面へ遷移
    /// </summary>
    private void HandleToModeSelect() => ShowCanvas((int)InGameCanvasEnum.ModeSelect);
    
    /// <summary>
    /// ショップ画面へ遷移
    /// </summary>
    private void HandleToShop() => ShowCanvas((int)InGameCanvasEnum.Shop);
    
    /// <summary>
    /// アイテムメニュー画面へ遷移
    /// </summary>
    private void HandleToItemMenu() => ShowCanvas((int)InGameCanvasEnum.ItemMenu);
    
    /// <summary>
    /// 設定画面へ遷移
    /// </summary>
    private void HandleToSettings() => ShowCanvas((int)InGameCanvasEnum.Settings);
    
    /// <summary>
    /// インゲームへ遷移
    /// </summary>
    private void HandleToInGame() => SceneManager.LoadScene(1);
    
    /// <summary>
    /// クレジットパネルを開く
    /// </summary>
    private void HandleOpenCredit() => PushCanvas((int)InGameCanvasEnum.Credit);
    
    /// <summary>
    /// ゲーム終了パネルを開く
    /// </summary>
    private void HandleOpenQuit() => PushCanvas((int)InGameCanvasEnum.Quit);

    /// <summary>
    /// グラフィック設定画面を開く
    /// </summary>
    private void HandleOpenGraphicSettings() => PushCanvas((int)InGameCanvasEnum.SettingsGraphic);
    
    /// <summary>
    /// サウンド設定を開く
    /// </summary>
    private void HandleOpenSoundSettings() => PushCanvas((int)InGameCanvasEnum.SettingsSound);
    
    /// <summary>
    /// 環境設定を開く
    /// </summary>
    private void HandleOpenEnvironmentSettings() => PushCanvas((int)InGameCanvasEnum.SettingsEnvironment);
    
    /// <summary>
    /// データリセットパネルを開く
    /// </summary>
    private void HandleOpenResetPanel() => PushCanvas((int)InGameCanvasEnum.SettingsReset);
    
    /// <summary>
    /// プレイヤーデータ確認パネルを開く
    /// </summary>
    private void HandleOpenPlayerData() => PushCanvas((int)InGameCanvasEnum.PlayerData);
    
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
            _ccHome.OnPlayerDataButtonClicked -= HandleOpenPlayerData;
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

        if (_ccSettingsGraphic != null)
        {
            _ccSettingsGraphic.OnCloseButtonClicked -= HandleToSettings;
        }
        
        if (_ccSettingsEnvironment != null)
        {
            _ccSettingsEnvironment.OnCloseButtonClicked -= HandleToSettings;
        }

        if (_ccSettingsSound != null)
        {
            _ccSettingsSound.OnCloseButtonClicked -= HandleToSettings;
        }
        
        if (_ccSettingsResetPanel != null)
        {
            _ccSettingsResetPanel.OnButtonClicked -= HandleToSettings;
        }

        if (_ccPlayerData != null)
        {
            _ccPlayerData.OnCloseButtonClicked -= HandlePopCanvas;
        }
    }
}
