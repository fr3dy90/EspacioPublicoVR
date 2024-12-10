using UnityEngine;
using UnityEngine.Rendering;

public class MainState : GameStateBase
{
    [SerializeField] private LocalState ActualState;
    [SerializeField] private AudioManager AudioManager;
    [SerializeField] private UIControllerBase uiController;
    public void Dependencies(AudioManager _audioManager, UIControllerBase _uiController)
    {
        AudioManager = _audioManager;
        uiController = _uiController;
    }
    
    public override void EnterState()
    {
        base.EnterState();
        SetState(ActualState);
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

    private void OnIntroVoice()
    {
        AudioManager.PlayOffVoice();
        uiController.OnShow();
        
        //todo: Add Listener al boton de la ui para cambiar de estado y passar a la primera actividad. 
    }
    
    
}
