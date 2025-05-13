using UnityEngine;

public class PlaneState : PlayerState
{
    protected const float PLANE_JUMP_HEIGHT = 0;
    protected const float PLANE_GRAVITY_MULTIPLIER = 0.2f;
    private const float FLY_TIME = 3f;

    private float timer = 0;
    private PlayerState previousState;

    public override void Start(PlayerStateController controller)
    {
        base.Start(controller);
        jumpHeight = PLANE_JUMP_HEIGHT;
        gravityMutliplier = PLANE_GRAVITY_MULTIPLIER;
    }

    public override void Update()
    {
        base.Update();

        timer += Time.deltaTime;

        if (timer >= FLY_TIME)
        {
            if (previousState != null)
            {
                controller.ChangeState(previousState);
                previousState = null;
            }
            else
            {
                Debug.LogWarning("Plane state was entered without a previous state. Defaulting to bunny state.");
                controller.ChangeState(controller.BunnyState);
            }
        }
    }

    public override void EnterState(PlayerState prevState)
    {
        base.EnterState(prevState);

        previousState = prevState;
        timer = 0;
    }
}
