using UnityEngine;
using System.Collections;

public class CameraSwitcher : MonoBehaviour {
    public Camera hirdPersonCamera;
    public Camera topCamera;

	// Use this for initialization
	void Start () {
        if (SelectCamera.typeCam == true)
        {
            topCamera.enabled = false;
            hirdPersonCamera.enabled = true;
        }
        else if (SelectCamera.typeCam == false)
        {
            topCamera.enabled = true;
            hirdPersonCamera.enabled = false;
        }
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
