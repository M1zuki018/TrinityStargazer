using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

/// <summary>
/// タイトル画面のUIManager
/// </summary>
public class TitleUIManager : SceneUIManagerBase
{
    // 画面のインデックス定数
    private const int TITLE_SCREEN_INDEX = 0;
    private const int HOME_SCREEN_INDEX = 1;
    private const int MODESELECT_SCREEN_INDEX = 2;
    private const int SHOP_SCREEN_INDEX = 3;
    private const int ITEMMENU_SCREEN_INDEX = 4;
    private const int SETTINGS_SCREEN_INDEX = 5;

    private CanvasController_Title _ccTitle;
    private CanvasController_Home _ccHome;
    private CanvasController_ModeSelect _ccModeSelect;
    private CanvasController_Shop _ccShop;
    private CanvasController_ItemMenu _ccItemMenu;
    private CanvasController_Settings _ccSettings;
    
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
        _ccTitle = (CanvasController_Title)_canvasObjects[TITLE_SCREEN_INDEX];
        _ccHome = (CanvasController_Home)_canvasObjects[HOME_SCREEN_INDEX];
        _ccModeSelect = (CanvasController_ModeSelect)_canvasObjects[MODESELECT_SCREEN_INDEX];
        _ccShop = (CanvasController_Shop)_canvasObjects[SHOP_SCREEN_INDEX];
        _ccItemMenu = (CanvasController_ItemMenu)_canvasObjects[ITEMMENU_SCREEN_INDEX];
        _ccSettings = (CanvasController_Settings)_canvasObjects[SETTINGS_SCREEN_INDEX];
    }
    
    protected override void RegisterWindowEvents()
    {
        InitializeCanvasControllers();

        if (_ccTitle != null)
        {
            _ccTitle.OnHomeButtonClicked += HandleToHome;
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
    }

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

    private void OnDestroy()
    {
        if (_ccTitle != null)
        {
            _ccTitle.OnHomeButtonClicked -= HandleToHome;
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
    }
}
