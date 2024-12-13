using System;
using UnityEngine;

public abstract class GameStateBase : MonoBehaviour
{
    public Action<States> FinishState;
    protected States nextState;
    
    [Header("Prefabs")] 
    [SerializeField] protected AudioManager prefAudioManager;
    [SerializeField] protected UIController prefUiController;
    [SerializeField] protected PositionProvider prefPositionProvider;
    [SerializeField] protected UIModel scriptableUIModel; 
    
    [Header("Depencies")] 
    [SerializeField] protected UIController uiController;
    
    [Header("Scene State")]
    [SerializeField] protected LocalState ActualState;

    [Header("AudioLibrary")] 
    [SerializeField] protected AudioClip[] audioLibrary;

    [Header("PositionProvider")] 
    [SerializeField] protected PositionProviderStruct positionProvider;

    protected virtual void Awake()
    {
        if (AudioManager.Instance == null)
        {
            Instantiate(prefAudioManager);
        }

        if (uiController == null)
            uiController = Instantiate(prefUiController);
        

        if (prefPositionProvider == null)
            Instantiate(prefPositionProvider);

        PositionProvider.Instance.VrCanvas = uiController.transform;
        
        uiController.Initialize();
    }

    protected virtual void Start()
    {
        if (Globals.DebugModeActivated)
            EnterState();
        else
            GameManager.GetState?.Invoke(this);
    }

    public virtual void EnterState()
    {
        
    }
    
    protected virtual void SetState(LocalState _nextState)
    {
        
    }

    public virtual void ExitState()
    {
        FinishState?.Invoke(nextState);
    }
}
