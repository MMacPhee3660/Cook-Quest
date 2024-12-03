using UnityEngine;

public abstract class PlayerBaseState
{
    public float baseSpeed;
    public Rigidbody rb;
    public abstract void EnterState(PlayerStateManager player);

    public abstract void UpdateState(PlayerStateManager player);
}
