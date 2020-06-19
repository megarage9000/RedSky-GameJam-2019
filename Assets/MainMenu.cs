using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    int score;
    public void PlayGame ()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame ()
    {
        Application.Quit();
    }

}
