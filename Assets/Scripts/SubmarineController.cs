using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using TMPro;

public class SubmarineController : MonoBehaviour
{
    public float leverAngle = 0;
    public float verticalSpeed = 0;
    public GameObject horizontalController;
    public GameObject verticalController;

    private Rigidbody rb;
    private float minHValue;
    private float maxHValue;
    private float minVValue;
    private float maxVValue;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        minHValue = horizontalController.minimum;
        maxHValue = horizontalController.maximum;
        minVValue = verticalController.minimum;
        maxVValue = verticalController.maximum;
    }

    private void Update()
    {
        UpdateSpeed();
    }

    private void UpdateSpeed()
    {
        float currentHValue = horizontalController.current;
        float horizontalSpeed = math.remap(-2f, 2f, minHValue, maxHValue, currentHValue);
        Vector3 movement = new Vector3(0.0f, 0.0f, 0.0f);
        float currentHSpeed = rb.velocity.z;
        if (currentHSpeed < horizontalSpeed)
        {
            movement.z += 0.1f;
        } else if (currentHSpeed > horizontalSpeed)
        {
            movement.z -= 0.1f;
        }
        float currentVSpeed = rb.velocity.y;
        if (currentVSpeed < verticalSpeed)
        {
            movement.y += 0.1f;
        }
        else if (currentVSpeed > verticalSpeed)
        {
            movement.y -= 0.1f;
        }
        rb.AddForce(movement);
    }


}
