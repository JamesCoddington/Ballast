using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using TMPro;
using Valve.VR.InteractionSystem;

public class SubmarineController : MonoBehaviour
{
    public float verticalSpeed = 0;
    public CircularDrive horizontalController;
    public CircularDrive verticalController;
    public float horizontalSpeed;

    private Rigidbody rb;
    private float minHValue;
    private float maxHValue;
    private float minVValue;
    private float maxVValue;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        minHValue = horizontalController.minAngle;
        maxHValue = horizontalController.maxAngle;
        //minVValue = verticalController.limits.min;
        //maxVValue = verticalController.limits.max;
    }

    private void Update()
    {
        UpdateSpeed();
    }

    private void UpdateSpeed()
    {
        float currentHValue = horizontalController.outAngle;
        horizontalSpeed = math.remap(minHValue, maxHValue, -2f, 2f, currentHValue);
        Vector3 movement = new Vector3(0.0f, 0.0f, 0.0f);
        float currentHSpeed = rb.velocity.z;
        if (currentHSpeed < horizontalSpeed)
        {
            movement.z += 0.1f;
        } else if (currentHSpeed > horizontalSpeed)
        {
            movement.z -= 0.1f;
        }
        /*
        float currentVSpeed = rb.velocity.y;
        if (currentVSpeed < verticalSpeed)
        {
            movement.y += 0.1f;
        }
        else if (currentVSpeed > verticalSpeed)
        {
            movement.y -= 0.1f;
        }
        */
        rb.AddForce(movement);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }
}
