using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public AudioClip clip;
    public AudioSource audioSource;

    void Awake()
    {
        audioSource.clip = clip;
    }
    
    public void TargetHit()
    {
        audioSource.Play();
    }
}