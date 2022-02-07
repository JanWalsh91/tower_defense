using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenueditor : MonoBehaviour
{
    
    public void Startbutton()
    {
        SceneManager.LoadScene("1");
    }

    public void Gameoptionsbutton()
    {
        SceneManager.LoadScene("Settingsmenu");
    }
    public void Quitgamebutton()
    {
        Application.Quit();
    }
}
