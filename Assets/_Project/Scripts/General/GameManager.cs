using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action<GameStateBase> GetState;

    [SerializeField] private States currentState;
    private SceneLoaderController loadingController;
    private GameStateBase currentGameState;

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
            /*case IntroState introState:
                introState.Dependencies();
                break;
            case MainState mainState:
                mainState.Dependencies();
                break;
            case StoryState storyState:
                storyState.Dependencies(achievements);
                break;
            case PlayersConfigurationState characterSelectorState:
                characterSelectorState.Dependencies(playerEntities);
                break;
            case GameplayState gameplayState:
                gameplayState.Dependencies(playerEntities, achievements);
                break;
            case ResultsState resultsState:
                resultsState.Dependencies(playerEntities);
                break;*/
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
