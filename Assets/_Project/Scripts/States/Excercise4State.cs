using UnityEngine;

public class Excercise4State : GameStateBase
{
    [SerializeField] private LocalState ActualState;
    [SerializeField] private AudioManager AudioManager;
    [SerializeField] private UIIntro uiController;
    
    //onlu for testing
    [SerializeField] private States testnextState;
    public void Dependencies()
    {

    }
    
    public override void EnterState()
    {
        base.EnterState();
        SetState(ActualState);
        uiController.SetButtron(OnFinish);
    }

    void OnFinish()
    {
        nextState = testnextState;
        ExitState();
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
       
        
        //todo: Add Listener al boton de la ui para cambiar de estado y passar a la primera actividad. 
    }
}
