using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : CoreComponent
{
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckDistance;
    [SerializeField] LayerMask groundMask;

    [SerializeField] Transform climableWallCheck;
    [SerializeField] Transform climableWallCheckUp;
    [SerializeField] float climableWallCheckDistance;
    [SerializeField] LayerMask climableMask;

    private Movement movement;

    protected override void Awake()
    {
        base.Awake();
    }

    void Start() 
    {
        movement = core.GetCoreComponent<Movement>();
    }


    public bool isGround
    {
        get => Physics2D.OverlapCircle(groundCheck.position, groundCheckDistance, groundMask);
    }

    public bool isClimableWallCheck
    {
        get => Physics2D.Raycast(climableWallCheck.position, movement.faceDirection, climableWallCheckDistance, climableMask);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(climableWallCheck.position, Vector2.left * climableWallCheckDistance);
    }
}
