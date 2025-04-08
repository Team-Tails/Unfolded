using UnityEngine;

public class BunnyState : PlayerState
{
    protected const float BUNNY_JUMP_HEIGHT = 0;

    public override void Start()
    {
        base.Start();
        jumpHeight = BUNNY_JUMP_HEIGHT;
    }
}
