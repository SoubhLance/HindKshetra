using UnityEngine;

namespace YkinikY
{
    public class CameraController : MonoBehaviour
    {
        [Header("Target")]
        public Transform target; // The player to follow
        
        [Header("Follow Settings")]
        public float smoothSpeed = 0.125f; // How smoothly the camera follows the player
        public Vector3 offset = new Vector3(0, 0, -10); // Camera offset from player
        
        private float initialY; // Store the initial Y position of the camera
        
        void Start()
        {
            // Store the initial Y position of the camera
            initialY = transform.position.y;
        }
        
        void LateUpdate()
        {
            if (target == null) return;
            
            // Calculate the desired position (only follow X position)
            Vector3 desiredPosition = new Vector3(
                target.position.x + offset.x,
                initialY, // Keep the camera at its initial Y position
                offset.z
            );
            
            // Smoothly move the camera
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
} 