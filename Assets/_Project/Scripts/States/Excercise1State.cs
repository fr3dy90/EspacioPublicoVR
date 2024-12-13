using Cysharp.Threading.Tasks;
using UnityEngine;

public class Excercise1State : GameStateBase
{
   
    [Header("Specific Dependencies")]
    [SerializeField] private InteractableGameObject[] sceneObjects;
    
    [Header("GameCore")] 
    [SerializeField] private int objectsFonded = 0;


    protected override void Awake()
    {
        base.Awake();
        objectsFonded = 0;
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
        nextState = States.Exercise_2;
        base.ExitState();
    }

    protected override void SetState(LocalState _nextState)
    {
        base.SetState(_nextState);
        switch (_nextState)
        {
            case LocalState.Intro:
                OnStartIntro();
                break;

            case LocalState.SocialInteraction:
                OnSocialInteraction();
                break;

            case LocalState.Closing:
                OnClosing();
                break;
        }

        ActualState = _nextState;
    }

    private async void OnStartIntro()
    {
        uiController.Initialize();

        // El indice define como se crea la vista de la pantalla. 
        uiController.SetView(0);
        AudioManager.Instance.PlayOffVoice(audioLibrary[0]);
        await UniTask.WaitForSeconds(audioLibrary[0].length);
        uiController.SetView(1, () => { SetState(LocalState.SocialInteraction); });
    }

    private void OnSocialInteraction()
    {
        HandleLevelColliders(true);
    }

    public async void GetObjectsFounded(InteractableGameObject interactableGO)
    {
        if (interactableGO.isGoodPlaced) objectsFonded++;
        
        uiController.SetView(ViewType.Medium, $"{objectsFonded}/{sceneObjects.Length}");
        interactableGO.GetComponent<BoxCollider>().enabled = false;
        
        if (objectsFonded == 3)
        {
            uiController.SetView(2, async () =>
            {
                AudioManager.Instance.PlayOffVoice(audioLibrary[1]);
                await UniTask.WaitForSeconds(audioLibrary[1].length);
                uiController.SetView(3, () =>
                {
                    SetState(LocalState.Closing);

                });
            });
        }
    }
    
    private async void OnClosing()
    {
        uiController.SetView(3);
        AudioManager.Instance.PlayOffVoice(audioLibrary[2]);
        await UniTask.WaitForSeconds(audioLibrary[2].length);
        uiController.SetView(3, () =>
        {
            uiController.SetView(ViewType.Medium, $"Puntaje\n{ScoreManager.Instance.GetScore()}", OnFinish);
        });
    }

    private async void OnFinish()
    {
        uiController.SetView(4);
        AudioManager.Instance.PlayOffVoice(audioLibrary[3]);
        await UniTask.WaitForSeconds(audioLibrary[3].length);
        uiController.SetView(4, ExitState);
    }


    private void HandleLevelColliders(bool _isActive)
    {
        foreach (InteractableGameObject interactableGO in sceneObjects)
        {
            interactableGO.GetComponent<BoxCollider>().enabled = _isActive;
        }
    }
}