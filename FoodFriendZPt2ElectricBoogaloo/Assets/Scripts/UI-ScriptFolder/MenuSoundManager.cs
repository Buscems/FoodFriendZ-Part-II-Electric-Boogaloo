using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSoundManager : MonoBehaviour
{

    public AudioSource audioSource;
    public AudioClip[] clips;

    public void BrowseSound()
    {
        try
        {
            audioSource.clip = clips[0];
            audioSource.Play();
        }
        catch { }
    }

    public void ClickSound()
    {
        try
        {
            audioSource.clip = clips[1];
            audioSource.Play();
        }
        catch { }
    }
}
