using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource introOne;
    public AudioSource introTwo;
    //AudioSource aud;

    public void playIntroOne()
    {
        introOne.Play();
    }

    public void playIntroTwo()
    {
        introTwo.Play();
    }
}
