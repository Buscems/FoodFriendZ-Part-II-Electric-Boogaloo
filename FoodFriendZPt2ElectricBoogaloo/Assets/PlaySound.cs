﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{

    public AudioSource sound;


    public void PlayAudio()
    {
        try
        {
            sound.Play();
        }
        catch { }
    }
}
