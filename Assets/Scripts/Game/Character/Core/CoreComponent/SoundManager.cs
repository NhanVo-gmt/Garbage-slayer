using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : CoreComponent
{
    [SerializeField] AudioClip moveClip;
    [SerializeField] AudioClip jumpClip;
    [SerializeField] AudioClip dashClip;
    [SerializeField] AudioClip hitClip;
    [SerializeField] AudioClip meleeClip;

    AudioSource source;
    
    protected override void Awake() 
    {
        base.Awake();

        source = GetComponent<AudioSource>();
    }

    public void PlayMoveClip()
    {
        source.Stop();
        source.loop = true;
        source.clip = moveClip;
        source.Play();
    }

    public void PlayJumpClip()
    {
        source.Stop();
        source.loop = false;
        source.PlayOneShot(jumpClip);
    }

    public void PlayDashClip()
    {
        source.Stop();
        source.loop = false;
        source.PlayOneShot(dashClip);
    }

    public void PlayHitClip()
    {
        source.Stop();
        source.loop = false;
        source.PlayOneShot(hitClip);
    }

    public void PlayMeleeClip()
    {
        source.Stop();
        source.loop = false;
        source.PlayOneShot(meleeClip);
    }
}
