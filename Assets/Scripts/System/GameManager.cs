using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

public class GameManager : ViewBase
{
    public static GameManager Instance;

    [SerializeField][ExpandableSO] private GameModeSO _modeSO;
    
    public GameModeEnum GameMode { get; private set; } = GameModeEnum.Normal;
    
    private ReactiveProperty<GameStateEnum> _currentGameState = new ReactiveProperty<GameStateEnum>(GameStateEnum.Title);
    
    public override UniTask OnAwake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
        
        return base.OnAwake();
    }

    /// <summary>
    /// ゲームモードを変更する
    /// </summary>
    public void SetGameMode(GameModeEnum mode) => GameMode = mode;
    
    /// <summary>
    /// 現在のゲーム状態を変更する
    /// </summary>
    public void ChangeGameState(GameStateEnum gameState) => _currentGameState.Value = gameState;
    
    /// <summary>
    /// 現在選択中のモードのデータを取得する
    /// </summary>
    public ModeData GetGameModeData() => _modeSO.GetModeData(GameMode);
}
