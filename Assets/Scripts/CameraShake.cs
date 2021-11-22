using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; 

public class CameraShake : MonoBehaviour
{
    CinemachineImpulseSource impulse;
    private void Start()
    {
        impulse = GetComponent<CinemachineImpulseSource>();
    }

    void Shake()
    {
        impulse.GenerateImpulse(10);
    }
}



