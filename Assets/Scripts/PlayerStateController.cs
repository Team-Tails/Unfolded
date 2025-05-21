using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.XR;
using System.Data.Common;

/// <summary>
/// Controls the state of the player e.g changing it, and what it is.
/// </summary>
public class PlayerStateController : Singleton<PlayerStateController>
{
    private PlayerState currentState;
    public PlayerState CurrentState { get => currentState; }

    private BunnyState bunnyState = new BunnyState();
    private RhinoState rhinoState = new RhinoState();
    private PlaneState planeState = new PlaneState();
    public BunnyState BunnyState { get => bunnyState; set => bunnyState = value; }
    public RhinoState RhinoState { get => rhinoState; set => rhinoState = value; }
    public PlaneState PlaneState { get => planeState; set => planeState = value; }

    [Header("UI Icons")]
    [SerializeField] private Image rabbitIcon;
    [SerializeField] private Image rhinoIcon;
    
    // First state is new state, second state is old state
    [HideInInspector] public UnityEvent<PlayerState, PlayerState> OnStateChange = new UnityEvent<PlayerState, PlayerState>();

    private void Start()
    {
        ChangeState(bunnyState);

        bunnyState.Start(this);
        rhinoState.Start(this);
        planeState.Start(this);

        UpdateIconOpacity(currentState);
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

        OnStateChange?.Invoke(currentState, prevState);

        UpdateIconOpacity(currentState);

        SoundManager.Instance.PlaySound("PlayerStateChange");
    }

    private void UpdateIconOpacity(PlayerState newState)
    {
        if (rabbitIcon != null && rhinoIcon != null)
        {
            Color rabbitColor = rabbitIcon.color;
            Color rhinoColor = rhinoIcon.color;

            if (newState == BunnyState)
            {
                rabbitColor.a = 1f;
                rhinoColor.a = 0.25f;
            }
            else if (newState == RhinoState)
            {
                rabbitColor.a = 0.25f;
                rhinoColor.a = 1f;
            }
            else
            {
                rabbitColor.a = 0.25f;
                rhinoColor.a = 0.25f;
            }

            rabbitIcon.color = rabbitColor;
            rhinoIcon.color = rhinoColor;
        }
    }
}
