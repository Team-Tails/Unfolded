using UnityEngine;

public class PlaneState : PlayerState
{
    protected const float PLANE_JUMP_HEIGHT = 0;
    private const float FLY_TIME = 3f;

    private float timer = 0;

    public override void Start()
    {
        base.Start();
        jumpHeight = PLANE_JUMP_HEIGHT;
    }

    public override void Update()
    {
        base.Update();

        timer += Time.deltaTime;

        if (timer >= FLY_TIME)
        {

        }
    }

    public override void EnterState()
    {
        base.EnterState();

        timer = 0;
    }
}
