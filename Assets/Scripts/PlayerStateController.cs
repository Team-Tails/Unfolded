using UnityEngine;

public class PlayerStateController: MonoBehaviour 
{
    private PlayerState currentState;

    public BunnyState bunnyState = new BunnyState();
    public RhinoState rhinoState = new RhinoState();
    public PlaneState planeState = new PlaneState();

    private void Start()
    {
        ChangeState(bunnyState);

        bunnyState.Start();
        rhinoState.Start();
        planeState.Start();
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.Update();
        }
    }

    public void ChangeState(PlayerState newState)
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
