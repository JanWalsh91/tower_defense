using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenueditor : MonoBehaviour
{
    
    public void Startbutton()
    {
        SceneManager.LoadScene("scene002clashroyale");
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
