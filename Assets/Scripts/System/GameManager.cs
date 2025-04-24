using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

public class GameManager : ViewBase
{
    public static GameManager Instance { get; private set; }

    [SerializeField][ExpandableSO] private GameModeSO _modeSO;
    [SerializeField] private GameModeEnum _currentGameMode = GameModeEnum.Normal;
    
    private ReactiveProperty<GameStateEnum> _currentGameState = new ReactiveProperty<GameStateEnum>(GameStateEnum.Title);
    
    /// <summary>
    /// 最初の読み込みかどうか（タイトル画面を表示するかどうかを決める）
    /// </summary>
    [SerializeField] private bool _isFirstLoad = true;
    public bool IsFirstLoad => _isFirstLoad;
    
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
        
        _currentGameState
            .Skip(1) // 初期値はスキップ
            .Take(1) // 最初の一回だけのみ処理
            .Subscribe(newState => 
            {
                // Titleから他のステートに変わった時点でフラグをオフにする
                if (newState != GameStateEnum.Title)
                {
                    _isFirstLoad = false;
                }
            })
            .AddTo(this);
        
        return base.OnAwake();
    }

    /// <summary>
    /// ゲームモードを変更する
    /// </summary>
    public void SetGameMode(GameModeEnum mode) => _currentGameMode = mode;
    
    /// <summary>
    /// 現在のゲーム状態を変更する
    /// </summary>
    public void SetGameState(GameStateEnum gameState) => _currentGameState.Value = gameState;
    
    /// <summary>
    /// 現在選択中のモードのデータを取得する
    /// </summary>
    public ModeData GetGameModeData() => _modeSO.GetModeData(_currentGameMode);
}
