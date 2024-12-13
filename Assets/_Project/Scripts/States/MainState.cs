using Cysharp.Threading.Tasks;
using UnityEngine;

public class MainState : GameStateBase
{
    [Header("Prefabs")] 
    [SerializeField] private AudioManager prefAudioManager;
    [SerializeField] private UIController prefUiController;
    
    [Header("Depencies")] 
    [SerializeField] private UIController uiController;
    
    [Header("Scene State")]
    [SerializeField] private LocalState ActualState;

    [Header("AudioLibrary")] 
    [SerializeField] private AudioClip[] audioLibrary;


    protected override void Awake()
    {
        base.Awake();
        uiController.Initialize();
    }

    public void Dependencies()
    {
        if (AudioManager.Instance == null)
        {
            Instantiate(prefAudioManager);
        }

        if (uiController == null)
            uiController = Instantiate(prefUiController);
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
    
    private void SetState(LocalState _nextState)
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
