using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMusicManager : MonoBehaviour
{
    public static BGMusicManager instance { get; private set; }

    private void Start()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else if (SceneManager.GetActiveScene().buildIndex <= 2)
        {
            Destroy(gameObject);
        }
        else 
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}