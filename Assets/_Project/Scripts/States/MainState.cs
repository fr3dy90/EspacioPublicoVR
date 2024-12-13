using Cysharp.Threading.Tasks;

public class MainState : GameStateBase
{
    protected override void Awake()
    {
        base.Awake();
    }

    public void Dependencies()
    {
        
    }
    
    public override void EnterState()
    {
        base.EnterState();
        SetState(ActualState);
    }

    public override void ExitState()
    {
        nextState = States.Exercise_1;
        base.ExitState();
    }
    
    protected override void SetState(LocalState _nextState)
    {
        switch (_nextState)
        {
            case LocalState.Intro:
                OnStartIntro();
                break;
            
            case LocalState.Observation:
                break;
            
            case LocalState.SocialInteraction:
                break;
            
            case LocalState.Closing:
                break;
        }

        ActualState = _nextState;
    }

    private void OnStartIntro()
    {
        PositionProvider.Instance.SetPosition(positionProvider);
        
        uiController.Initialize();
        // El indice define como se crea la vista de la pantalla. 
        uiController.SetView(0, () =>
        {
            uiController.SetView(1, ()=> { OnWaitAudioClip(); });
        });
    }

    private async UniTask OnWaitAudioClip()
    {
        uiController.SetView(2);
        AudioManager.Instance.PlayOffVoice(audioLibrary[0]);
        await UniTask.WaitForSeconds(audioLibrary[0].length);
        uiController.SetView(2,ExitState);
    }
}
