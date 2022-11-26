using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : CoreComponent
{
    public Rigidbody2D rb {get; private set;} 
    public Vector2 faceDirection {get; private set;}

    float slowFactor = 1;
    bool canSetVelocity = true;
    float gravityScale;

    Coroutine addForceCoroutine;
    
    protected override void Awake() 
    {
        base.Awake();
        
        rb = GetComponentInParent<Rigidbody2D>();
    }

    void Start()
    {
        InitializeDirection();

        gravityScale = rb.gravityScale;
    }

    #region Velocity

    public void SlowDown()
    {
        slowFactor = 0.5f;
    }

    public void MoveNormal()
    {
        slowFactor = 1f;
    }

    public void SetVelocity(Vector2 velocity, bool needToChangeFaceDirection = true) 
    {
        if (needToChangeFaceDirection)
        {
            ChangeDirection(velocity.x);
        }

        if (!canSetVelocity) return;

        rb.velocity = velocity * slowFactor;
    }

    public void SetVelocityX(float xVelocity, bool needToChangeFaceDirection = true)
    {
        if (needToChangeFaceDirection)
        {
            ChangeDirection(xVelocity);
        }

        if (!canSetVelocity) return;
        
        rb.velocity = new Vector2(xVelocity * slowFactor, rb.velocity.y);
    }

    public void SetVelocityY(float yVelocity) 
    {
        if (!canSetVelocity) return;
        
        rb.velocity = new Vector2(rb.velocity.x, yVelocity);
    }

    public void SetVelocityZero() 
    {
        rb.velocity = Vector2.zero;
    }

    public Vector2 GetVelocity()
    {
        return rb.velocity;
    }

    public void AddForce(Vector2 direction, float amount)
    {
        StopAddForce();

        addForceCoroutine = StartCoroutine(AddForceCoroutine(direction, amount));
    }

    IEnumerator AddForceCoroutine(Vector2 direction, float amount)
    {
        canSetVelocity = false;

        rb.AddForce(direction * amount);
        
        yield return new WaitForSeconds(0.3f);

        SetVelocityZero();

        canSetVelocity = true;
    }

    void StopAddForce()
    {
        if (addForceCoroutine != null)
        {
            StopCoroutine(addForceCoroutine);
        }

        canSetVelocity = true;
    }

    #endregion

    #region Gravity

    public void SetGravityZero()
    {
        rb.gravityScale = 0;
    }

    public void SetGravityNormal()
    {
        rb.gravityScale = gravityScale;
    }


    #endregion
    

    #region Direction

    private void InitializeDirection()
    {
        faceDirection = Vector2.left;
    }

    public void ChangeDirection(float xInput)
    {
        if (xInput < 0)
        {
            if (faceDirection == Vector2.right) Flip();
            
            faceDirection = Vector2.left;
            
        }
        else if (xInput > 0)
        {
            if (faceDirection == Vector2.left) Flip();
            
            faceDirection = Vector2.right;
        }
    }

    void Flip() 
    {
        rb.transform.Rotate(0, 180, 0);
    }

    #endregion
}
