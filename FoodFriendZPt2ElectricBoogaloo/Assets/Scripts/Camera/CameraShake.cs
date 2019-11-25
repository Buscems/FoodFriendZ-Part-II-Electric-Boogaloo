using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float maxShakeTime;
    private float shakeDuration = 0f;
    public float intensity = 0.7f;
    public float decreaseFactor = 1.0f;

    public GameObject screenFlash;
    public float screenFlashOpacity;
    private float currentFlashTime = 0;

    [Header("ControllerShake")]
    public float contShakeIntensity;
    public float contShakeDuration;

    [HideInInspector]
    public float maxShakeTime2;
    [HideInInspector]
    private float shakeDuration2 = 0f;
    [HideInInspector]
    public float intensity2 = 0.7f;
    [HideInInspector]
    public float decreaseFactor2 = 1.0f;

    Vector3 originalPos;

    public MainPlayer player;

    public void Start()
    {
        Color c = screenFlash.GetComponent<SpriteRenderer>().color;
        c.a = 0;
        screenFlash.GetComponent<SpriteRenderer>().color = c;
    }

    void Awake()
    {
        originalPos = transform.localPosition;
        //shakeDuration = maxShakeTime;
    }

    void Update()
    {
        currentFlashTime -= Time.deltaTime*1.5f;

        if(currentFlashTime > 0)
        {
            Color c = screenFlash.GetComponent<SpriteRenderer>().color;
            c.a = currentFlashTime;
            screenFlash.GetComponent<SpriteRenderer>().color = c;
        }

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

        try
        {
            player.ControllerShake(contShakeIntensity, contShakeDuration);
        }
        catch { }

    }

    public void FlashScreen()
    {
        currentFlashTime = screenFlashOpacity;
    }

}
