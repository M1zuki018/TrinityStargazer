using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

/// <summary>
/// ゲームマネージャー
/// </summary>
public class GameManager : ViewBase, IGameManager
{
    [SerializeField] private GameSettings _settings;
    public GameSettings Settings => _settings;
    
    [SerializeField][ExpandableSO] private GameModeSO _modeSO;
    
    [SerializeField] private GameModeEnum _currentGameMode = GameModeEnum.Normal;
    private ReactiveProperty<GameStateEnum> _currentGameState = new ReactiveProperty<GameStateEnum>(GameStateEnum.Title);
    private bool _isFirstLoad = true; // 最初の読み込みかどうか
    public bool IsFirstLoad => _isFirstLoad;
    public int VictoryPoints { get; private set; } = 0;

    public override UniTask OnAwake()
    {
        // 既に別のインスタンスが存在する場合、このオブジェクトを破棄
        if (GameManagerServiceLocator.IsInitialized() && GameManagerServiceLocator.Instance != this)
        {
            Destroy(gameObject);
            return base.OnAwake();
        }
        
        // サービスロケーターに自身を登録
        GameManagerServiceLocator.SetInstance(this);
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

    [ContextMenu("アイテムテスト")]
    public void ItemTest()
    {
        for (int i = 0; i < 3; i++)
        {
            InventoryManager.Instance.AddItem(ItemTypeEnum.SealPage, RarityEnum.C);
            InventoryManager.Instance.AddItem(ItemTypeEnum.ReverseBroom, RarityEnum.SSR);
            InventoryManager.Instance.AddItem(ItemTypeEnum.CelestialForecast, RarityEnum.C);
            InventoryManager.Instance.AddItem(ItemTypeEnum.SmartPhone, RarityEnum.SSR);
            InventoryManager.Instance.AddItem(ItemTypeEnum.ResonanceCable, RarityEnum.C);
            InventoryManager.Instance.AddItem(ItemTypeEnum.ChallengeRose, RarityEnum.SSR);
            InventoryManager.Instance.AddItem(ItemTypeEnum.StarAttraction, RarityEnum.R);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ItemTest();
        }
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
    /// 勝利数をセットする
    /// </summary>
    public int SetVictoryPoints(int points) => VictoryPoints = points;

    /// <summary>
    /// 現在選択中のモードのデータを取得する
    /// </summary>
    public ModeData GetGameModeData() => _modeSO.GetModeData(_currentGameMode);
    
    private void OnDestroy()
    {
        // 自身がインスタンスとして登録されている場合のみリセット
        if (GameManagerServiceLocator.Instance == this)
        {
            GameManagerServiceLocator.Reset();
        }
    }
}
