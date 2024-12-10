using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action<GameStateBase> GetState;

    [SerializeField] private States currentState;
    private SceneLoaderController loadingController;
    private GameStateBase currentGameState;

    [Header("Dependencies")] 
    [SerializeField] private AudioManager AudioManager;
    [SerializeField] private UIControllerBase UiBase;

    private void Awake()
    {
        loadingController = GetComponent<SceneLoaderController>();
        Application.targetFrameRate = 120;
    }

    private void Start()
    {
        StartGame();
    }

    private void StartGame()
    {
        AddListeners();
        loadingController.LoadScene((int)currentState);
    }

    private void AddListeners()
    {
        loadingController.SwitchScenesCompleted += OnSceneChangeComplete;
        GameManager.GetState += OnGetState;
    }

    private void OnGetState(GameStateBase state)
    {
        currentGameState = state;
        currentGameState.FinishState += OnChangeState;

        DependencyInjection(state);
    }

    private void DependencyInjection(GameStateBase state)
    {
        switch (state)
        {
            case MainState mainState:
                mainState.Dependencies(AudioManager);
                break;
            case Excercise1State excercise1State:
                excercise1State.Dependencies();
                break;
            case Excercise2State excercise2State:
                excercise2State.Dependencies();
                break;
            case Excercise3State excercise3State:
                excercise3State.Dependencies();
                break;
            case Excercise4State excercise4State:
                excercise4State.Dependencies();
                break;
            case MinigameState minigameState:
                minigameState.Dependencies();
                break;
            default:
                break;
        }
    }

    private void OnChangeState(States nextState)
    {
        currentGameState.FinishState -= OnChangeState;

        loadingController.SwitchScenes((int)currentState, (int)nextState);

        currentState = nextState;
    }

    private void OnSceneChangeComplete()
    {
        if (currentGameState != null)
        {
            currentGameState.EnterState();
        }
    }

    private void RemoveListeners()
    {
        if(currentGameState != null)
            currentGameState.FinishState -= OnChangeState;

        GameManager.GetState -= OnGetState;
        loadingController.SwitchScenesCompleted -= OnSceneChangeComplete;
    }

    private void OnApplicationPause(bool pause)
    {
        
    }

    private void OnApplicationQuit()
    {
        RemoveListeners();

        currentGameState.ExitState();
    }
}
