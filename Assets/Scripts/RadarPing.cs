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
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Hit2");
        Debug.Log("Hit2 " + other.gameObject.GetComponent<Animation>());
        other.gameObject.GetComponent<Animation>().Play("SonarPip");
    }
}
