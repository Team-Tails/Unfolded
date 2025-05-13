using UnityEngine;

public class BunnyState : PlayerState
{
    protected const float BUNNY_JUMP_HEIGHT = 5;
    protected const float BUNNY_GRAVITY_MULTIPLIER = 2f;

    public override void Start(PlayerStateController controller)
    {
        base.Start(controller);
        jumpHeight = BUNNY_JUMP_HEIGHT;
        gravityMutliplier = BUNNY_GRAVITY_MULTIPLIER;
    }
}
