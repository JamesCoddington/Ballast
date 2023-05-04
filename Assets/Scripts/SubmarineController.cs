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
    public LinearDrive horizontalController;
    public float horizontalSpeed;
    public float maxSpeed = 5f;
    public float minSpeed = -2.5f;

    [Header("Elevation Controller")]
    public LinearDrive verticalController;
    public float elevation;
    public float minElevation;
    public float maxElevation;

    [Header("Rotation Controller")]
    public LinearDrive rotationController;
    public float rotationalSpeed;

    [Header("Lights")]
    public Light[] normalLights;
    public Light[] emergencyLights;

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
    }

    private void Update()
    {
        if (!powerShutOff)
        {
            UpdateSpeed();
        }
    }

    private void UpdateSpeed()
    {
        float currentHorizontal = horizontalController.linearMapping.value;
        horizontalSpeed = math.remap(0f, 1f, minSpeed, maxSpeed, currentHorizontal);
        hoverMovement.MoveSpeed = horizontalSpeed;
        hoverMovement.Move(new Vector2(0.0f, (horizontalSpeed != 0 ? 1f : 0f)));

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

    public void togglePower()
    {
        powerShutOff = !powerShutOff;
        print("toggling Power to " + powerShutOff);
        if (powerShutOff)
        {
            // Lights
            foreach (Light light in normalLights)
            {
                light.enabled = false;
            }
            foreach (Light light in emergencyLights)
            {
                light.enabled = true;
            }

            // Movement
            rotationalSpeed = 0f;
            horizontalSpeed = 0f;

        } else
        {
            // Lights
            foreach (Light light in normalLights)
            {
                light.enabled = true;
            }
            foreach (Light light in emergencyLights)
            {
                light.enabled = false;
            }

            // Movement
            // Automatically reset by UpdateSpeed()

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
        }
    }
}
