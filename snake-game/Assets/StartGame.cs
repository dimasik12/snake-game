using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour 
{
    public Animation hide;

    public void ButtonClickGame()
    {
        Application.LoadLevel("Game");
    }

    public void HideButton()
    {
        hide.Play("HideButton");
    }
}
