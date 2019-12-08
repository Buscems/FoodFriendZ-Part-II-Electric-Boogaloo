using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDebris : MonoBehaviour
{
    public AudioSource breakableSound;
    // Start is called before the first frame update
    void Start()
    {
        playASound();
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, 3);
    }

    void playASound()
    {
        breakableSound.Play();
    }
}
