using UnityEngine;

namespace Mryotaisu.Scripts
{
    [RequireComponent(typeof(CharacterController), typeof(Animator))]
    public class MuryotaisuController : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float speed = 5f;          
        public float jumpSpeed = 10f;     
        public float gravity = 25f;       
        
        [Header("Double Jump Settings")]
        public int maxJumps = 2;           
        private int _jumpCount = 0;        

        [Header("Interaction Settings")]
        public float startKocchi = 2f;    

        private Animator _animator;
        private CharacterController _controller;
        private Transform _mainCameraTransform;
        
        private Vector3 _moveDirection = Vector3.zero;
        private float _second; 

        // 动画 Hash 缓存
        private readonly int _smileFlagHash = Animator.StringToHash("smileFlag");
        private readonly int _kocchiFlagHash = Animator.StringToHash("kocchiFlag");
        private readonly int _jumpFlagHash = Animator.StringToHash("jumpFlag");
        private readonly int _walkFlagHash = Animator.StringToHash("walkFlag");
        private readonly int _idleFlagHash = Animator.StringToHash("idleFlag");
        private readonly int _idleBFlagHash = Animator.StringToHash("idleBFlag");

        void Start()
        {
            _animator = GetComponent<Animator>();
            _controller = GetComponent<CharacterController>();

            if (Camera.main != null)
            {
                _mainCameraTransform = Camera.main.transform;
            }
        }

        void Update()
        {
            HandleFacialExpressions();
            HandleMovementAndAnimation();
        }

        private void HandleFacialExpressions()
        {
            _animator.SetBool(_smileFlagHash, Input.GetKey(KeyCode.Q));

            if (_mainCameraTransform != null)
            {
                Vector3 apos = transform.position;
                Vector3 bpos = _mainCameraTransform.position;
                float dist = Vector3.Distance(apos, bpos);
                _animator.SetBool(_kocchiFlagHash, dist < startKocchi);
            }
        }

        private void HandleMovementAndAnimation()
        {
      
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

         
            Vector3 inputDirection = (transform.right * horizontal + transform.forward * vertical).normalized;
            bool isWalking = inputDirection.magnitude >= 0.1f;

        
            if (_controller.isGrounded)
            {
                _jumpCount = 0;  
                
                
                if (_moveDirection.y < 0f) 
                {
                    _moveDirection.y = -2f; 
                }

                _animator.SetBool(_jumpFlagHash, false);

                if (isWalking)
                {
                    _animator.SetBool(_walkFlagHash, true);
                    _animator.SetBool(_idleFlagHash, false);
                    _second = 0f; 
                }
                else
                {
                    _animator.SetBool(_walkFlagHash, false);
                    _animator.SetBool(_idleFlagHash, true);
                    
                    _second += Time.deltaTime;
                    if (_second >= 15f)
                    {
                        _animator.SetTrigger(_idleBFlagHash);
                        _second = 0f;
                    }
                }
            }
            else
            {
               
                if (_jumpCount == 0) _jumpCount = 1;

       
                _moveDirection.y -= gravity * Time.deltaTime;
            }

             
            if (Input.GetButtonDown("Jump") && _jumpCount < maxJumps)
            {
                 
                _moveDirection.y = jumpSpeed;
                _jumpCount++;
                 
                _animator.SetBool(_jumpFlagHash, true);
                _animator.SetBool(_walkFlagHash, false);
                _animator.SetBool(_idleFlagHash, false);
                
            
                if (_jumpCount == 2)
                {
                    _animator.Play("Jump", -1, 0f); 
                }
                
                _second = 0f; 
            }

         
            Vector3 movement = inputDirection * speed;
            movement.y = _moveDirection.y;
            _controller.Move(movement * Time.deltaTime);
        }
    }
}