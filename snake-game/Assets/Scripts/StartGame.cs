using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour 
{
    public Animation hide;

    public void ButtonClickGame()
    {
        Application.LoadLevel("IntricacyMenu");
    }

    public void HideButton()
    {
        hide.Play("HideButton");
    }
}
