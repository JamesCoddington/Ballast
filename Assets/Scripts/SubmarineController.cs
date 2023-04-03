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
    private float minHorizontal;
    private float maxHorizontal;

    [Header("Elevation Controller")]
    public LinearDrive verticalController;
    public float elevation;
    public float minElevation;
    public float maxElevation;

    [Header("Rotation Controller")]
    public LinearDrive rotationController;
    public float rotationalSpeed;

    [Header("Creature")]
    public GameObject creature;

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
        horizontalSpeed = math.remap(minHorizontal, maxHorizontal, -1f, 3f, currentHorizontal);
        hoverMovement?.Move(new Vector2(0.0f, horizontalSpeed));

        float rotationInput = rotationController.linearMapping.value;
        rotationalSpeed = math.remap(0f, 1f, -1f, 1f, rotationInput);
        hoverLook.HorizontalTurnSpeed = rotationalSpeed;
        hoverLook?.Turn(new Vector2(rotationInput, 0));

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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Creature Trigger"))
        {
            creature.SetActive(true);
            Debug.Log("made the creature (I think)");
        }
    }
}
