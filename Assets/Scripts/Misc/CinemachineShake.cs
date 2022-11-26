using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CinemachineShake : SingletonObject<CinemachineShake>
{
    static CinemachineVirtualCamera cam;
    static CinemachineBasicMultiChannelPerlin camShake;

    static float duration; 

    protected override void Awake() 
    {
        base.Awake();

        cam = GetComponent<CinemachineVirtualCamera>();
        camShake = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    
    public static void Shake(float shakeDuration, float shakeAmount, float frequency)
    {
        duration = shakeDuration;
        camShake.m_AmplitudeGain = shakeAmount;
        camShake.m_FrequencyGain = frequency;
    }

    void Update() 
    {
        if (duration > 0)
        {
            duration -= Time.deltaTime;
            if (duration <= 0)
            {
                StopShaking();
            }
        }
    }

    private void StopShaking()
    {
        camShake.m_AmplitudeGain = 0;
        camShake.m_FrequencyGain = 0;
    }
}
