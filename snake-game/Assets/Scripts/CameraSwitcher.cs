﻿using UnityEngine;
using System.Collections;

public class CameraSwitcher : MonoBehaviour {
    public Camera hirdPersonCamera;
    public Camera topCamera;

	void Start () {
        // проверка какая камера была выбрана
        if (SelectCamera.typeCam == true)
        {
            topCamera.enabled = false;
            hirdPersonCamera.enabled = true;
            hirdPersonCamera.GetComponent<AudioListener>().enabled = true;
            topCamera.GetComponent<AudioListener>().enabled = false;
        }
        else if (SelectCamera.typeCam == false)
        {
            topCamera.enabled = true;
            hirdPersonCamera.enabled = false;
            hirdPersonCamera.GetComponent<AudioListener>().enabled = false;
            topCamera.GetComponent<AudioListener>().enabled = true;
        }
	
	}	

}
