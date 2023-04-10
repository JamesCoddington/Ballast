using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeakWeld : MonoBehaviour
{

    public float weldTimer = 0.0f;
    public bool isWelding = false;
    public LeakController leakController;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isWelding == true) {
            weldTimer = weldTimer +1;
        }
        if (weldTimer >= 75) {
            WeldingFinish();
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Welder") {
             //do things

             weldTimer = 0;
             isWelding = true;
             print("imWelding");
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Welder") {
             //do things
             weldTimer = 0;
             isWelding = false;
             print("imNotWelding");
        }
    }

    void WeldingFinish()
    {
        gameObject.active = false;
        isWelding = false;
        weldTimer = 0;
        leakController.notFound = false;
    }
}
