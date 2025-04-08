using UnityEngine;

public abstract class PlayerState
{
    public abstract void Update();

    public abstract void Jump();

    public abstract void EnterState();

    public abstract void ExitState();
}
