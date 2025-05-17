using UnityEngine;

public class PlaneState : PlayerState
{
    protected const float PLANE_JUMP_HEIGHT = 0;
    protected const float PLANE_GRAVITY_MULTIPLIER = 0.2f;
    private const float FLY_TIME = 5f;

    private float timer = 0;
    private PlayerState previousState;
    private PlayerController playerController;

    public override void Start(PlayerStateController controller)
    {
        base.Start(controller);
        jumpHeight = PLANE_JUMP_HEIGHT;
        gravityMutliplier = PLANE_GRAVITY_MULTIPLIER;
        playerController = controller.GetComponent<PlayerController>();
    }

    public override void Update()
    {
        base.Update();

        if (controller.CurrentState != this) return;

        timer += Time.deltaTime;

        if (timer >= FLY_TIME)
        {
            EndPlaneState();
        }
    }

    public override void EnterState(PlayerState prevState)
    {
        base.EnterState(prevState);

        previousState = prevState;
        timer = 0;
    }

    public void EndPlaneState()
    {
        if (playerController.isFlying)
        {
            playerController.isFlying = false;
        }
        if (previousState != null)
        {
            controller.ChangeState(previousState);
            previousState = null;
        }
        else
        {
            Debug.LogWarning("Plane state was entered without a previous state. Defaulting to bunny state.");
            controller.ChangeState(controller.BunnyState);
            timer = 0;
        }
    }
}
