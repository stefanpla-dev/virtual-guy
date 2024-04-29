using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGSoundManager : MonoBehaviour
{
    public static BGSoundManager instance { get; private set; }
    private AudioSource audioSource;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex <= 2) // Only pause audio in scenes with build index 2 or smaller
        {
            AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
            foreach (AudioSource source in allAudioSources)
            {
                if (source != audioSource)
                {
                    source.Pause();
                }
            }
        }
        else if (scene.buildIndex >=2)
        {
            AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();
            foreach (AudioSource source in allAudioSources)
            {
                if (source != audioSource)
                {
                    source.UnPause();
                }
            }
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}