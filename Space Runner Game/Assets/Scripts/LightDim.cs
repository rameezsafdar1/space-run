using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightDim : MonoBehaviour
{
    [SerializeField] private Light lightsource;
    [SerializeField] private float dimSpeed;
    private bool dim;

    public void StartDim(bool b)
    {
        dim = b;
    }

    private void Update()
    {
        if (dim)
        {
            lightsource.intensity -= dimSpeed * Time.deltaTime;
        }
    }
}
