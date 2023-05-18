using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeakController : MonoBehaviour
{
    public GameObject leakParent;
    public AudioSource source;
    public AudioClip clip;
    public int randoInt = 0;

    private int maxChildren;
    public bool notFound = false;
    //public bool canCollide = true;
    //public float collideCD = 1.2f;
    //public float collideCDCurrent = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        maxChildren = leakParent.transform.childCount;
    }

    void OnCollisionEnter(Collision collision)
    {
        takeDamage();
    }

    public void takeDamage()
    {
        randoInt = Random.Range(1, 2);
        print($"POG FROG {randoInt}");
        if (notFound == false && randoInt == 2)
        {
            randoInt = 0;
            int randomLeak = Random.Range(0, maxChildren);
            Transform leak = leakParent.transform.GetChild(randomLeak);
            print(randomLeak);
            if (leak.gameObject.activeSelf == false)
            {
                leak.gameObject.SetActive(true);
                source.Play();
                notFound = false;
            }
            else
            {
                notFound = true;
                print(notFound);
            }
        }
    }
}
