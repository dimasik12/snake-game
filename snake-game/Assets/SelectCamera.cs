using UnityEngine;
using System.Collections;

public class SelectCamera : MonoBehaviour 
{
    public Animation camera;
    public Animator cam;
    public static bool typeCam = false;

    public void ClickButtonCamera()
    {
        cam.cullingMode = AnimatorCullingMode.AlwaysAnimate;
        cam.Play("ClickButtonCamera");
    }
    public void SwitchCameraThirdPerson()
    {
        cam.cullingMode = AnimatorCullingMode.AlwaysAnimate;
        if (typeCam == false)
        {
            cam.Play("SwitchCamera");
            typeCam = true;
            Debug.Log(typeCam);
        }
        else if(typeCam == true)
        {
            cam.Play("SwitchTopCamera");
            typeCam = false;
            Debug.Log(typeCam);
        }

        //camera.Play("SwitchCamera");
    }
}
