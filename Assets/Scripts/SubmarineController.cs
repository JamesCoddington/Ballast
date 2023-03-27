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
    public LinearDrive verticalController;
    public LinearDrive rotationController;
    public float horizontalSpeed;
    public float rotationalSpeed;
    public float elevation;

    private Rigidbody rb;
    private float minHorizontal;
    private float maxHorizontal;
    private float minVertical = 0;
    private float maxVertical = 100;
    private float minRotation;
    private float maxRotation;

    HoverMovement hoverMovement;
    HoverLook hoverLook;
    HoverGrid hoverGrid;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hoverMovement = GetComponent<HoverMovement>();
        hoverLook = GetComponent<HoverLook>();
        hoverGrid = GetComponent<HoverGrid>();

        minHorizontal = horizontalController.minAngle;
        maxHorizontal = horizontalController.maxAngle;
    }

    private void Update()
    {
        UpdateSpeed();
        
    }

    private void UpdateSpeed()
    {
        float currentHorizontal = horizontalController.outAngle;
        horizontalSpeed = math.remap(minHorizontal, maxHorizontal, -0.5f, 1f, currentHorizontal);
        hoverMovement?.Move(new Vector2(0.0f, horizontalSpeed));

        // TODO: Complete rotation
        // float currentRotation = rotationController.linearMapping.value;
        //float currentRotation = 0.5f;
        // Map from Linear Mapping progression from 0 to 1 to the Left/Right mapping of -1 to 1
        //rotationalSpeed = math.remap(0f, 1f, -1f, 1f, currentRotation);
        hoverLook?.Turn(new Vector2(rotationalSpeed/1000, 0.0f));

        // TODO: Complete elevation
        // float currentVertical = verticalController.linearMapping.value;
        //float currentVertical = 0.5f;
        // Map from Linear Mapping progression from 0 to 1 to minimum/maximum height in cave
        //elevation = math.remap(0f, 1f, minVertical, maxVertical, currentVertical);
        hoverGrid.TargetHeight = elevation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }
}
