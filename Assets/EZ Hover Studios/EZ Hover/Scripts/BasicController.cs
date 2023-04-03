using UnityEngine;

namespace EZHover
{
    public class BasicController : MonoBehaviour
    {
        HoverMovement hoverMovement;
        HoverLook hoverLook;

        public float inputX;
        public float inputY;

        private void Awake()
        {
            hoverMovement = GetComponentInChildren<HoverMovement>();
            hoverLook = GetComponentInChildren<HoverLook>();
        }

        void Update()
        {
            inputX = Input.GetAxis("Mouse X");
            inputY = Input.GetAxis("Mouse Y");
            hoverMovement?.Move(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
            hoverLook?.Turn(new Vector2(inputX, inputY));
        }
    }
}