using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PooledObject : MonoBehaviour
{
    public IObjectPool<GameObject> pool;
    Animator anim;

    void Awake() 
    {
        anim = GetComponent<Animator>();
    }

    public void Initialize(float lifeTime)
    {
        Invoke("Release", lifeTime);
    }


    // Used in animation clip
    public void Release()
    {
        ResetAnimator();

        pool.Release(gameObject);
    }

    void ResetAnimator()
    {
        if (anim != null)
        {
            anim.Rebind();
            anim.Update(0f);
        }
    }
}
