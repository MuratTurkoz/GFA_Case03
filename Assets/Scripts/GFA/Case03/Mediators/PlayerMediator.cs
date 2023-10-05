using GFA.Case03.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

namespace GFA.Case03.Mediators
{
    //Mediator Sýnýfý oluþturduk
    public class PlayerMediator : MonoBehaviour
    {
        private GameInput _gameInput;
        private Plane _plane = new Plane(Vector3.up, Vector3.zero);
        private Camera _camera;
        private CharacterMovement _characterMovement;
        //private PlayerAnimation _playerAnimation;
        [SerializeField] private Vector2 _look;
        //[SerializeField] private bool _isRun;
        //[SerializeField] private bool _isJump;
        [SerializeField]
        private float playerRunSpeed = 4f;
        [SerializeField]
        private bool _isJumped;
        public bool IsJumped { get { return _isJumped; } set { _isJumped = value; } }
        [SerializeField]
        private Vector2 _movement;
        public Vector2 Movement { get { return _movement; } set { _movement = value; } }
        [SerializeField]
        private bool _isRun;
        public bool IsRun { get { return _isRun; } set { _isRun = value; } }
        [SerializeField]
        private bool _isCrouch;
        public bool IsCoruch { get { return _isCrouch; } set { _isCrouch = value; } }
        [SerializeField] Animator _playerAnimation;
        [SerializeField] Animator _camAnimation;

        private void Awake()
        {
            //Debug.Log(PlayerInstance.Health);
            //_playerAnimation = GetComponent<PlayerAnimation>();
            _gameInput = new GameInput();
            _characterMovement = GetComponent<CharacterMovement>();
            _camera = Camera.main;
        }
        private void OnEnable()
        {
            _gameInput.Enable();
            _gameInput.Player.Attack.performed += OnAttack;
            _gameInput.Player.Look.performed += OnLook;
            _gameInput.Player.Run.performed += OnRun;
            _gameInput.Player.Jump.performed += OnJump;
            _gameInput.Player.Crouch.performed += OnCrouch;
        }
        private void OnDisable()
        {
            _gameInput.Disable();
            _gameInput.Player.Run.performed -= OnRun;
            _gameInput.Player.Look.performed -= OnLook;
            _gameInput.Player.Attack.performed -= OnAttack;
            _gameInput.Player.Jump.performed -= OnJump;
            _gameInput.Player.Crouch.performed -= OnCrouch;
        }
        private void OnLook(InputAction.CallbackContext context)
        {
            _look = Vector2.zero;
            _look = context.ReadValue<Vector2>();
        }
        private void OnCrouch(InputAction.CallbackContext context)
        {
            _isCrouch = context.action.triggered;
            //_playerAnimation.SetBool("Crouch", _isCrouch);
        }
        public Vector2 GetMouseDelta()
        {
            return _look;
        }
        private void Update()
        {
            SetIsRun();
            SetJump();
            SetCrouch();
            HandleMovement();
        }
        private void SetJump()
        {
            _isJumped = _gameInput.Player.Jump.IsPressed();
            _playerAnimation.SetBool("IsJump", _isJumped);

        }
        public void OnRun(InputAction.CallbackContext context)
        {
            _isRun = context.action.triggered;
            //_playerAnimation.SetBool("Crouch", true);
        }
        private void SetIsRun()
        {
            _isRun = _gameInput.Player.Run.IsPressed();
       
        }
        private void SetCrouch()
        {
            _isCrouch = _gameInput.Player.Crouch.IsPressed();
            _playerAnimation.SetBool("IsCrouch", _isCrouch);
            //_animPlayer.SetBool("IsCrouch", IsCrouch);
            if (_isCrouch)
            {
                _characterMovement.CharacterControllerOld.height = 1.5f;
                _characterMovement.CharacterControllerOld.center = new Vector3(0, 0.75f, 0);
                _camAnimation.Play("Crouch");

            }
            else
            {
                _characterMovement.CharacterControllerOld.height = 1.6f;
                _characterMovement.CharacterControllerOld.center = new Vector3(0, 0.8f, 0);
                _camAnimation.Play("Run");
            }
        }
        private void OnAttack(InputAction.CallbackContext obj)
        {
            //_playerAnimation.Attack();
            //_playerAnimation.DeAttack();
        }
        private void OnJump(InputAction.CallbackContext context)
        {
            _isJumped = context.action.triggered;
        }
        public bool GetPlayerRun()
        {
            return _isRun;
        }
        private void HandleMovement()
        {
            if (GameSession.Instance.IsDied == false)
            {
                Move();
                Rotate();
            }

        }
        //Move
        private void Move()
        {
            Movement = _gameInput.Player.Movement.ReadValue<Vector2>();
            if (_isRun == true)
            {
                _characterMovement.MovementSpeed = 10;


            }
            else
            {
                _characterMovement.MovementSpeed = 4;
            }
            //_characterMovement.MovementInput =Movement;


        }
        //Rotatiton
        private void Rotate()
        {
            var ray = _camera.ScreenPointToRay(_gameInput.Player.PointerPosition.ReadValue<Vector2>());
            if (_plane.Raycast(ray, out float enter))
            {
                var worldPosition = ray.GetPoint(enter);
                Debug.DrawRay(worldPosition, Vector3.up, Color.red);
                var dir = (worldPosition - transform.position).normalized;
                //Quaternion.LookRotation(dir).eulerAngles.y

                var angle = -Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg + 90;
                _characterMovement.Rotation = angle;
            }
        }
    }




}


