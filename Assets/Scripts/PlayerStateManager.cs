using UnityEngine;

/// <summary>
/// Manages the state of the player e.g when it should change.
/// </summary>
public class PlayerStateManager : MonoBehaviour
{
    [SerializeField] private PlayerStateController controller;

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
