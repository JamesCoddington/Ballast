using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeakController : MonoBehaviour
{
    public GameObject leakParent;
    public AudioSource source;
    public AudioClip clip;

    private int maxChildren;
    private int leakPercent;
    public bool notFound = false;
    //public bool canCollide = true;
    //public float collideCD = 1.2f;
    //public float collideCDCurrent = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        maxChildren = leakParent.transform.childCount;
    }

    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        leakPercent += Random.Range(1, 100);
        if (leakPercent >= 100)
        {
            print(leakPercent);
            takeDamage();
            leakPercent = 0;
        }
    }

    public void takeDamage()
    {
        print("POG FROG");
        if (notFound == false)
        {
            int randomLeak = Random.Range(0, maxChildren);
            Transform leak = leakParent.transform.GetChild(randomLeak);
            print(randomLeak);
            if (leak.gameObject.activeSelf == false)
            {
                leak.gameObject.SetActive(true);
                source.PlayOneShot(clip);
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
