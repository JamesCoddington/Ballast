using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeakController : MonoBehaviour
{
    public GameObject leakParent;
    private int maxChildren;

    // Start is called before the first frame update
    void Start()
    {
        maxChildren = leakParent.transform.childCount;
    }

    void OnCollisionEnter(Collision collision)
    {
        print("POG FROG");
        // print(collision.relativeVelocity.magnitude);
        bool notFound = true;
        while (notFound)
        {
            int randomLeak = Random.Range(0, maxChildren);
            Transform leak = leakParent.transform.GetChild(randomLeak);
            if (leak.gameObject.activeSelf == false)
            {
                leak.gameObject.SetActive(true);
                notFound = false;
            }
        }
        
    }
}
