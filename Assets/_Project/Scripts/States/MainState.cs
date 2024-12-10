using System;
using UnityEngine;

public class MainState : GameStateBase
{
    [SerializeField] private LocalState ActualState;
    [SerializeField] private AudioManager AudioManager;
    [SerializeField] private UIControllerBase uiController;
    public void Dependencies(AudioManager _audioManager)
    {
        AudioManager = _audioManager;
    }
    
    public override void EnterState()
    {
        base.EnterState();
        SetState(ActualState);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            nextState = States.Exercise_1;
            ExitState();
        }
    }

    private void SetState(LocalState _nextState)
    {
        if (ActualState == _nextState) return;

        switch (_nextState)
        {
            case LocalState.IntroVoice:
                break;
            case LocalState.Close:
                break;
        }
    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("entra exit state");
    }

    private void OnIntroVoice()
    {
        AudioManager.PlayOffVoice();
        uiController.OnShow();
        
        //todo: Add Listener al boton de la ui para cambiar de estado y passar a la primera actividad. 
    }
    
    
}
