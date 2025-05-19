using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the state of the player e.g changing it, and what it is.
/// </summary>
public class PlayerStateController : MonoBehaviour
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

        UpdateIconOpacity(currentState);
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
