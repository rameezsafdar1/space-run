using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamShaker : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cam;
    private CinemachineBasicMultiChannelPerlin Perlin;

    private void Awake()
    {
        Perlin = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    public void SetAmp(float f)
    {
        Perlin.m_AmplitudeGain = f;
    }
}
