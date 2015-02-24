using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour 
{
    public Animation exit;
    public Player snake;
    private float speed;

    public void ExitWindow()
    {        
        exit.Play("WindowExit");
        speed = snake.speed;
        snake.speed = 0;
        //Time.timeScale = 0;        
    }

    public void ExitGeme()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    public void CancelExitWindow()
    {
        exit.Play("CancelExitWindow");
        snake.speed = speed;
    }
}
