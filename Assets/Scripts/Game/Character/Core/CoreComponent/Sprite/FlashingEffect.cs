using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimatorController))]
public class FlashingEffect : MonoBehaviour
{
    [SerializeField] float cooldown;
    
    Coroutine flashingCoroutine;

    SpriteRenderer sprite;

    void Awake() 
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    
    public void StartFlashing()
    {
        StopFlashing();
        flashingCoroutine = StartCoroutine(Flashing());
    }

    IEnumerator Flashing()
    {
        sprite.material.SetInt("_Hit", 1);
        yield return new WaitForSeconds(cooldown);
        sprite.material.SetInt("_Hit", 0);
    }

    void StopFlashing()
    {
        if (flashingCoroutine != null)
        {
            StopCoroutine(flashingCoroutine);
            sprite.material.SetInt("_Hit", 0);
        }
    }
}
