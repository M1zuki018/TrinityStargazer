using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

public class GameManager : ViewBase
{
    public static GameManager Instance { get; private set; }

    [SerializeField][ExpandableSO] private GameModeSO _modeSO;
    [SerializeField] private GameModeEnum _currentGameMode = GameModeEnum.Normal;
    
    private ReactiveProperty<GameStateEnum> _currentGameState = new ReactiveProperty<GameStateEnum>(GameStateEnum.Title);
    
    public override UniTask OnAwake()
    {
        if (Instance != null && Instance != this)
        {
            // 既に別のインスタンスが存在する場合、このオブジェクトを破棄
            Destroy(gameObject);
            return base.OnAwake();
        }
        
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        return base.OnAwake();
    }

    /// <summary>
    /// ゲームモードを変更する
    /// </summary>
    public void SetGameMode(GameModeEnum mode) => _currentGameMode = mode;
    
    /// <summary>
    /// 現在のゲーム状態を変更する
    /// </summary>
    public void ChangeGameState(GameStateEnum gameState) => _currentGameState.Value = gameState;
    
    /// <summary>
    /// 現在選択中のモードのデータを取得する
    /// </summary>
    public ModeData GetGameModeData() => _modeSO.GetModeData(_currentGameMode);
}
