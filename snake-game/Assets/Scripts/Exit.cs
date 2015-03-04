using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour 
{
    public Animator exitAnim;
    //public Player snake;
    private float speed;

    public void ExitWindow()
    {     
        exitAnim.cullingMode = AnimatorCullingMode.AlwaysAnimate;
        exitAnim.Play("Exit");
        //speed = snake.speed;
        speed = Player.speed;
        //snake.speed = 0;   
        Player.speed = 0;
    }

    public void ExitGeme()
    {
        Application.Quit();
    }

    public void CancelExitWindow()
    {
        exitAnim.Play("CancelExit");
        //snake.speed = speed;
        Player.speed = speed;
    }
}
