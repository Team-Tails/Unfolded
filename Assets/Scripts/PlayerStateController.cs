using UnityEngine;

/// <summary>
/// Controls the state of the player e.g changing it, and what it is.
/// </summary>
public class PlayerStateController: MonoBehaviour 
{
    private PlayerState currentState;

    private BunnyState bunnyState = new BunnyState();
    private RhinoState rhinoState = new RhinoState();
    private PlaneState planeState = new PlaneState();
    public BunnyState BunnyState { get => bunnyState; set => bunnyState = value; }
    public RhinoState RhinoState { get => rhinoState; set => rhinoState = value; }
    public PlaneState PlaneState { get => planeState; set => planeState = value; }

    private void Start()
    {
        ChangeState(bunnyState);

        bunnyState.Start(this);
        rhinoState.Start(this);
        planeState.Start(this);
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

        PlayerState prevState = currentState;

        if (currentState != null)
        {
            currentState.ExitState();
        }

        currentState = newState;
        currentState.EnterState(prevState);
    }
}
