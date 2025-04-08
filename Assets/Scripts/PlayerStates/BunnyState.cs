using UnityEngine;

public class BunnyState : PlayerState
{
    protected const float BUNNY_JUMP_HEIGHT = 0;

    public override void Start(PlayerStateController controller)
    {
        base.Start(controller);
        jumpHeight = BUNNY_JUMP_HEIGHT;
    }
}
