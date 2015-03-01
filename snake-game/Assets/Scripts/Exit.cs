using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour 
{
    public Animation exit;
    public Animator exitAnim;
    public Player snake;
    private float speed;

    public void ExitWindow()
    {        
        //exit.Play("WindowExit");
        exitAnim.cullingMode = AnimatorCullingMode.AlwaysAnimate;
        exitAnim.Play("Exit");
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
        //exit.Play("CancelExitWindow");
        exitAnim.Play("CancelExit");
        snake.speed = speed;
    }
}
