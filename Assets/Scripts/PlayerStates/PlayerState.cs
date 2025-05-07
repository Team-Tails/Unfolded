using UnityEngine;

public abstract class PlayerState
{
    protected float jumpHeight = 0;
    protected PlayerStateController controller;

    public virtual void Start(PlayerStateController controller)
    {
        this.controller = controller;
    }

    public virtual void Update()
    {

    }

    public virtual void Jump()
    {

    }

    public virtual void EnterState(PlayerState prevState)
    {

    }

    public virtual void ExitState()
    {
        // Play Animation Here like the poof
    }
}
