using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using TMPro;
using Valve.VR.InteractionSystem;
using EZHover;

public class SubmarineController : MonoBehaviour
{
    public CircularDrive horizontalController;
    public CircularDrive verticalController;
    public LinearDrive rotationController;
    public float horizontalSpeed;

    private Rigidbody rb;
    private float minHValue;
    private float maxHValue;
    private float minVValue;
    private float maxVValue;
    private float minRotation;
    private float maxRotation;

    HoverMovement hoverMovement;
    HoverLook hoverLook;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hoverMovement = GetComponentInChildren<HoverMovement>();
        hoverLook = GetComponentInChildren<HoverLook>();

        minHValue = horizontalController.minAngle;
        maxHValue = horizontalController.maxAngle;
    }

    private void Update()
    {
        UpdateSpeed();
        
    }

    private void UpdateSpeed()
    {
        float currentHValue = horizontalController.outAngle;
        // horizontalSpeed = currentHValue / 15f;
        horizontalSpeed = math.remap(minHValue, maxHValue, -0.5f, 1f, currentHValue);
        hoverMovement?.Move(new Vector2(0.0f, horizontalSpeed));


        float currentRotationValue = rotationController.linearMapping.value;
        // horizontalSpeed = currentHValue / 15f;
        horizontalSpeed = math.remap(minHValue, maxHValue, -1f, 1f, currentHValue);
        hoverMovement?.Move(new Vector2(0.0f, horizontalSpeed));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }
}
