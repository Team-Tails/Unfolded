using UnityEngine;

public enum PlayerStateEnum
{
    Bunny,
    Rhino,
    Plane,
}

public class PlayerStateController: MonoBehaviour 
{
    private PlayerState currentState;

    private void Update()
    {
        currentState.Update();
    }

    public void ChangeState(PlayerStateEnum newState)
    {
        PlayerState iState = ConvertStateToInterface(newState);

        if (currentState == iState) return;

        if (currentState != null)
        {
            currentState.ExitState();
        }
        currentState = iState;
        currentState.EnterState();
    }

    private PlayerState ConvertStateToInterface(PlayerStateEnum state)
    {
        PlayerState iState = null;

        switch (state)
        {
            case PlayerStateEnum.Bunny:

                break;
            case PlayerStateEnum.Rhino:

                break;
            case PlayerStateEnum.Plane:

                break;
        }

        return iState;
    }
}
