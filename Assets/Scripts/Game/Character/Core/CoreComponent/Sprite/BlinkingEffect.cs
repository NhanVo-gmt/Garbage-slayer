using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AnimatorController))]
public class BlinkingEffect : MonoBehaviour
{
    [SerializeField] float cooldown;
    [SerializeField] float blinkTime;
    
    Coroutine blinkingCoroutine;

    SpriteRenderer sprite;

    void Awake() 
    {
        sprite = GetComponent<SpriteRenderer>();
    }
    
    public void StartBlinking()
    {
        StopBlinking();
        blinkingCoroutine = StartCoroutine(Blinking());
    }

    IEnumerator Blinking()
    {
        float startTime = Time.time;

        while (startTime + cooldown > Time.time)
        {
            sprite.enabled = !sprite.enabled;
            yield return new WaitForSeconds(blinkTime);
        }

        yield return null;

        sprite.enabled = true;
    }

    void StopBlinking()
    {
        if (blinkingCoroutine != null)
        {
            StopCoroutine(blinkingCoroutine);
            sprite.enabled = true;
        }
    }
}
