using Cysharp.Threading.Tasks;
using UniRx;

public class GameManager : ViewBase
{
    public static GameManager Instance;
    private ReactiveProperty<GameStateEnum> _currentGameState = new ReactiveProperty<GameStateEnum>(GameStateEnum.Title);
    
    public override UniTask OnAwake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
        
        return base.OnAwake();
    }

    /// <summary>
    /// 現在のゲーム状態を変更する
    /// </summary>
    public void ChangeGameState(GameStateEnum gameState) => _currentGameState.Value = gameState;
}
