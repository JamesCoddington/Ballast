using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnderWaterEffects : MonoBehaviour
{

    public GameObject playerCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.tag == "MainCamera") {
            GetComponent<AudioLowPassFilter>().cutoffFrequency = (Mathf.Sin(Time.time) * 11010 + 11000);
            print("WorkingAsIntended");
        }
    }
}
