using UnityEngine;

public abstract class PlayerState
{
    protected float jumpHeight = 0;

    public virtual void Start()
    {

    }

    public virtual void Update()
    {

    }

    public virtual void Jump()
    {

    }

    public virtual void EnterState()
    {

    }

    public virtual void ExitState()
    {
        // Play Animation Here like the poof
    }
}
