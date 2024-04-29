using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{
    public void OnBackButton()
    {
        SceneManager.LoadScene(0);
    }
    public void OnControlsButton()
    {
        SceneManager.LoadScene(1);
    }
    public void OnReadMeButton()
    {
        SceneManager.LoadScene(2);
    }
    public void OnPlayButton()
    {
        SceneManager.LoadScene(3);
    }
}
