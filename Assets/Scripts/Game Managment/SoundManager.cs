using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    //line below allows other scripts to access the sound manger but prevents the sound manager from being modified in any script besides this one.
    public static SoundManager instance { get; private set; }
    private AudioSource source;

    private void Start()
    {
        instance = this;
        source = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip _sound)
    {
        source.PlayOneShot(_sound);
    }
}
