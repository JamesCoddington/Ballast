using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using TMPro;
using Valve.VR.InteractionSystem;
using EZHover;

public class SubmarineController : MonoBehaviour
{
    [Header("Horizontal Controller")]
    public CircularDrive horizontalController;
    public float horizontalSpeed;
    public float maxSpeed = 5f;
    private float minSpeed;
    private float minAngle;
    private float maxAngle;

    [Header("Elevation Controller")]
    public LinearDrive verticalController;
    public float elevation;
    public float minElevation;
    public float maxElevation;

    [Header("Rotation Controller")]
    public LinearDrive rotationController;
    public float rotationalSpeed;

    [Header("Submarine Properties")]
    public bool powerShutOff = false;

    private Rigidbody rb;


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

        minAngle = horizontalController.minAngle;
        maxAngle = horizontalController.maxAngle;
        minSpeed = maxSpeed / (maxAngle / minAngle);
    }

    private void Update()
    {
        UpdateSpeed();
        
    }

    private void UpdateSpeed()
    {
        float currentHorizontal = horizontalController.outAngle;
        horizontalSpeed = math.remap(minAngle, maxAngle, minSpeed, maxSpeed, currentHorizontal);
        hoverMovement?.Move(new Vector2(0.0f, horizontalSpeed));

        float rotationInput = rotationController.linearMapping.value;
        rotationalSpeed = math.remap(0f, 1f, -1f, 1f, rotationInput);
        hoverLook.HorizontalTurnSpeed = rotationalSpeed;
        if (rotationalSpeed < 0f)
        {
            rotationalSpeed *= -1f;
        }
        hoverLook?.Turn(new Vector2(rotationalSpeed, 0));

        float elevationInput = verticalController.linearMapping.value;
        elevation = math.remap(0f, 1f, maxElevation, minElevation, elevationInput);
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
