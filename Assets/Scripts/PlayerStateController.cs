using UnityEngine;

public class PlayerStateController: MonoBehaviour 
{
    private IPlayerState currentState;

    private void Update()
    {
        currentState.Update();
    }

    public void ChangeState(IPlayerState newState)
    {
        if (currentState == newState) return;

        if (currentState != null)
        {
            currentState.ExitState();
        }
        currentState = newState;
        currentState.EnterState();
    }
}
