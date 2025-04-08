using UnityEngine;

public class PlaneState : PlayerState
{
    protected const float PLANE_JUMP_HEIGHT = 0;

    public override void Start()
    {
        base.Start();
        jumpHeight = PLANE_JUMP_HEIGHT;
    }
}
