using UnityEngine;
using System.Collections;

public class Audio : MonoBehaviour {
    public AudioClip clip1;// clip2;
	
	// Update is called once per frame
	void Update () {
        if (Player.speed < 4)
        {
            audio.PlayOneShot(clip1);
        }
        //else if (Player.speed > 4)
        //{
         //   audio.Pause();            
           /// audio.Play();
          //  audio.PlayOneShot(clip2);
        //}
	}
}
