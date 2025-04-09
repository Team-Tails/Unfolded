using UnityEngine;

public class RhinoState : PlayerState
{
    protected const float RHINO_JUMP_HEIGHT = 0;

    public override void Start(PlayerStateController controller)
    {
        base.Start(controller);
        jumpHeight = RHINO_JUMP_HEIGHT;
    }
}
