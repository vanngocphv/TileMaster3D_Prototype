using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    private AudioSource audioSrc;

    private void Awake()
    {
        audioSrc = GetComponent<AudioSource>();
    }
    public void SetAudioSrc(bool isActive)
    {
        audioSrc.enabled = isActive;
    }

    public void PlayAudioClip()
    {
        if (audioSrc.enabled)
        {
            audioSrc.Play();
        }
    }
}
