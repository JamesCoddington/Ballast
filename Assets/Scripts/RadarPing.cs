/* 
    ------------------- Code Monkey -------------------
    
    Thank you for downloading the Code Monkey Utilities
    I hope you find them useful in your projects
    If you have any questions use the contact form
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarPing : MonoBehaviour {
    public AudioSource sound;

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Animation>().Play("SonarPip");
        sound.Play();
    }
}
