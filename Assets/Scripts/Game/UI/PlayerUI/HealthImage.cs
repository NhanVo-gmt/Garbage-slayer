using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthImage : MonoBehaviour
{
    int loseHealthId = Animator.StringToHash("break");
    int gainHealthId = Animator.StringToHash("recover");
    
    Animator anim;

    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Disable()
    {
        //anim.Play(loseHealthId);
        gameObject.SetActive(false);
    }

    public void Enable()
    {
        gameObject.SetActive(true);
        //anim.Play(gainHealthId);
    }

    #region Animation event

    public void OnAnimationFinishTrigger()
    {
        gameObject.SetActive(false);
    }

    #endregion
}
