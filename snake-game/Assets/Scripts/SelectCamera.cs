using UnityEngine;
using System.Collections;

public class SelectCamera : MonoBehaviour 
{
    //public Animation camera;
    public Animator cam;
    public static bool typeCam = false;
    private bool _buttonCam = false;

    public void ClickButtonCamera()
    {
        cam.cullingMode = AnimatorCullingMode.AlwaysAnimate;
        // проверка на нажатие кнопки "Камера"
        if (!_buttonCam)
        {
            cam.Play("ClickButtonCamera");
            _buttonCam = true;
        }
        else if(_buttonCam)
        {
            cam.Play("CloseButtonCamera");
            _buttonCam = false;
        }
    }
    public void SwitchCameraThirdPerson()
    {
        cam.cullingMode = AnimatorCullingMode.AlwaysAnimate;
        // проверка, какая камера была выбрана
        if (!typeCam)
        {
            cam.Play("SwitchCamera");
            typeCam = true;           
        }
        else if(typeCam)
        {
            cam.Play("SwitchTopCamera");
            typeCam = false;           
        }       
    }
}
