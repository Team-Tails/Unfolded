using UnityEngine;

/// <summary>
/// Manages interacting with the state of the player e.g when it should change.
/// </summary>
public class PlayerStateManager : MonoBehaviour
{
    public static PlayerStateManager Instance;

    [SerializeField] private PlayerStateController controller;

    public PlayerState CurrentState { get => controller.CurrentState; }

    private void Awake()
    {
        if (Instance != null && Instance != this) 
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    public void BunnyStatePress()
    {
        controller.ChangeState(controller.BunnyState);
    }

    public void RhinoStatePress()
    {
        controller.ChangeState(controller.RhinoState);
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlaneLauncher"))
        {
            controller.ChangeState(controller.PlaneState);
        }
    }
}
