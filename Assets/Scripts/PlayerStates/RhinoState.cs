using UnityEngine;

public class RhinoState : PlayerState
{
    protected const float RHINO_JUMP_HEIGHT = 0.5f;
    protected const float RHINO_GRAVITY_MULTIPLIER = 2f;

    public override void Start(PlayerStateController controller)
    {
        base.Start(controller);
        jumpHeight = RHINO_JUMP_HEIGHT;
        gravityMutliplier = RHINO_GRAVITY_MULTIPLIER;
    }
}
