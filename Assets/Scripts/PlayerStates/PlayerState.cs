using UnityEngine;

public abstract class PlayerState
{
    protected float jumpHeight = 0;
    protected float gravityMutliplier;
    public float JumpHeight {  get => jumpHeight; }
    public float GravityMultiplier {  get => gravityMutliplier; }
    protected PlayerStateController controller;

    public virtual void Start(PlayerStateController controller)
    {
        this.controller = controller;
    }

    public virtual void Update()
    {

    }

    public virtual void EnterState(PlayerState prevState)
    {
        Debug.Log("Entered state");
    }

    public virtual void ExitState()
    {
        // Play Animation Here like the poof
    }
}
