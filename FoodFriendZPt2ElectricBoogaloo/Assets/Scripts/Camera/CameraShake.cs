using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float maxShakeTime;
    private float shakeDuration = 0f;
    public float intensity = 0.7f;
    public float decreaseFactor = 1.0f;

    [HideInInspector]
    public float maxShakeTime2;
    [HideInInspector]
    private float shakeDuration2 = 0f;
    [HideInInspector]
    public float intensity2 = 0.7f;
    [HideInInspector]
    public float decreaseFactor2 = 1.0f;

    Vector3 originalPos;

    void Awake()
    {
        originalPos = transform.localPosition;
        //shakeDuration = maxShakeTime;
    }

    void Update()
    {
        originalPos = transform.localPosition;

        if (shakeDuration > 0)
        {
            transform.localPosition = originalPos + Random.insideUnitSphere * intensity;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }

        if (shakeDuration2 > 0)
        {
            transform.localPosition = originalPos + Random.insideUnitSphere * intensity2;

            shakeDuration2 -= Time.deltaTime * decreaseFactor;
        }
    }

    public void StartShake()
    {
        originalPos = transform.localPosition;
        shakeDuration = maxShakeTime;
    }

    public void StartShake(float _maxShakeTime,  float intensity)
    {
        originalPos = transform.localPosition;
        shakeDuration = maxShakeTime;
    }

}
