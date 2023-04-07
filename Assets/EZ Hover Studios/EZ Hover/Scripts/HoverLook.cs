﻿using UnityEngine;

namespace EZHover
{
    public class HoverLook : MonoBehaviour
    {
        [SerializeField] private bool enableInput = true;
        public bool EnableInput { get { return enableInput; } set { enableInput = value; } }

        [SerializeField] private float verticalTurnSpeed = 5f;
        public float VerticalTurnSpeed { get { return verticalTurnSpeed; } set { verticalTurnSpeed = value; } }

        [SerializeField] private float horizontalTurnSpeed = 5f;
        public float HorizontalTurnSpeed { get { return horizontalTurnSpeed; } set { horizontalTurnSpeed = value; } }

        private Rigidbody rb;
        public Vector2 turnDir;

        private void Awake()
        {
            rb = GetComponentInParent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (!enableInput)
            {
                return;
            }

            rb.AddTorque(Vector3.up * turnDir.x * horizontalTurnSpeed * rb.mass);
            // Debug.Log("float: " + turnDir.x);

            var right = new Vector3(transform.right.x, 0f, transform.right.z);
            rb.AddTorque(right * turnDir.y * verticalTurnSpeed * -1 * rb.mass);
        }

        public void Turn(Vector2 turnDirection)
        {
            turnDir = turnDirection.normalized;
        }
    }
}