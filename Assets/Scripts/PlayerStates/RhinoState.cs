using UnityEngine;

public class RhinoState : PlayerState
{
    protected const float RHINO_JUMP_HEIGHT = 0;

    public override void Start()
    {
        base.Start();
        jumpHeight = RHINO_JUMP_HEIGHT;
    }
}
