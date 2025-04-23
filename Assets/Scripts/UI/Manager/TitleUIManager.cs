/// <summary>
/// タイトル画面のUIManager
/// </summary>
public class TitleUIManager : SceneUIManagerBase
{
    // 画面のインデックス定数
    private const int TITLE_SCREEN_INDEX = 0;
    private const int HOME_SCREEN_INDEX = 1;

    private CanvasController_Title _ccTitle;
    
    protected override void RegisterWindowEvents()
    {
        _ccTitle = (CanvasController_Title)_canvasObjects[0];
        _ccTitle.OnHomeButtonClicked += HandleToHome;
    }

    /// <summary>
    /// ホーム画面へ遷移
    /// </summary>
    private void HandleToHome()
    {
        ShowCanvas(HOME_SCREEN_INDEX);
    }

    private void OnDestroy()
    {
        _ccTitle.OnHomeButtonClicked -= HandleToHome;
    }
}
