using UnityEngine;

public class Excercise2State : GameStateBase
{
    [SerializeField] private LocalState ActualState;
    [SerializeField] private AudioManager AudioManager;

    //onlu for testing
    [SerializeField] private States testnextState;
    public void Dependencies()
    {

    }
    
    public override void EnterState()
    {
        base.EnterState();
        SetState(ActualState);
    }

   

    private void SetState(LocalState _nextState)
    {
        if (ActualState == _nextState) return;

        
    }

    public override void ExitState()
    {
        base.ExitState();
        Debug.Log("entra exit state");
    }

  
}
