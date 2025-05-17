using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.XR;

/// <summary>
/// Controls the state of the player e.g changing it, and what it is.
/// </summary>
public class PlayerStateController: Singleton<PlayerStateController> 
{
    private PlayerState currentState;
    public PlayerState CurrentState { get => currentState; }

    private BunnyState bunnyState = new BunnyState();
    private RhinoState rhinoState = new RhinoState();
    private PlaneState planeState = new PlaneState();
    public BunnyState BunnyState { get => bunnyState; set => bunnyState = value; }
    public RhinoState RhinoState { get => rhinoState; set => rhinoState = value; }
    public PlaneState PlaneState { get => planeState; set => planeState = value; }

    [HideInInspector] public UnityEvent<PlayerState> OnStateChange = new UnityEvent<PlayerState>();

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

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlaneLauncher"))
        {
            GetComponent<PlayerController>().OnLaunch();
            ChangeState(PlaneState);
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

        OnStateChange?.Invoke(currentState);
        print(currentState);
    }
}
