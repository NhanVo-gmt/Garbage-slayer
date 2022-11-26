using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionChecker : CoreComponent
{
    [SerializeField] Transform groundCheck;
    [SerializeField] Vector2 groundCheckBox;
    [SerializeField] LayerMask groundMask;

    protected override void Awake()
    {
        base.Awake();
    }


    public bool isGround
    {
        get => Physics2D.OverlapBox(groundCheck.position, groundCheckBox, 0f, groundMask);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(groundCheck.position, groundCheckBox);
    }
}
