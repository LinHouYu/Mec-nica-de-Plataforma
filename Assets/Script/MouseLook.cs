using UnityEngine;

public class MouseLook : MonoBehaviour
{
        [Header("Target Settings")]
        public Transform target;        
        public Vector3 targetOffset = new Vector3(0, 1.5f, 0);  

        [Header("Camera Control")]
        public float sensitivity = 3f;     
        public float smoothTime = 0.1f;    
        public Vector2 pitchMinMax = new Vector2(-40, 85);  

        [Header("Zoom Settings")]
        public float distance = 5f;          
        public float minDistance = 2f;       
        public float maxDistance = 10f;      
        public float zoomSpeed = 5f;         

        private float _yaw;    
        private float _pitch; 
        private float _currentYaw;
        private float _currentPitch;
        
        private float _yawSmoothVelocity;
        private float _pitchSmoothVelocity;

        void Start()
        {
             
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            if (target != null)
            {
                _yaw = transform.eulerAngles.y;
                _pitch = transform.eulerAngles.x;
            }
        }

        void LateUpdate()
        {
            if (target == null) return;

 
            _yaw += Input.GetAxis("Mouse X") * sensitivity;
            _pitch -= Input.GetAxis("Mouse Y") * sensitivity;
            _pitch = Mathf.Clamp(_pitch, pitchMinMax.x, pitchMinMax.y);

 
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            distance -= scroll * zoomSpeed;
            distance = Mathf.Clamp(distance, minDistance, maxDistance);
 
            _currentYaw = Mathf.SmoothDampAngle(_currentYaw, _yaw, ref _yawSmoothVelocity, smoothTime);
            _currentPitch = Mathf.SmoothDampAngle(_currentPitch, _pitch, ref _pitchSmoothVelocity, smoothTime);
 
            Vector3 focusPosition = target.position + targetOffset;
            Quaternion rotation = Quaternion.Euler(_currentPitch, _currentYaw, 0);
            Vector3 position = focusPosition - rotation * Vector3.forward * distance;
 
            transform.position = position;
            transform.rotation = rotation;
 
            target.rotation = Quaternion.Euler(0, _currentYaw, 0);
        }
}
