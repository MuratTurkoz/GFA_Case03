using GFA.Case03.Movement;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GFA.Case03.Mediators
{
    //Mediator Sýnýfý oluþturduk
    public class PlayerMediator : MonoBehaviour
    {
        private GameInput _gameInput;
        private Plane _plane = new Plane(Vector3.up, Vector3.zero);
        private Camera _camera;
        private CharacterMovement _characterMovement;
        private PlayerAnimation _playerAnimation;
  
        private void Awake()
        {
            //Debug.Log(PlayerInstance.Health);
            _playerAnimation = GetComponent<PlayerAnimation>();
            _gameInput = new GameInput();
            _characterMovement = GetComponent<CharacterMovement>();
            _camera = Camera.main;
        }
        private void OnEnable()
        {
            _gameInput.Enable();
            _gameInput.Player.Attack.performed += OnAttack;
        }
        private void OnDisable()
        {
            _gameInput.Disable();
            _gameInput.Player.Attack.performed += OnAttack;
        }
        private void Update()
        {
          
            HandleMovement();
        }
        private void OnAttack(InputAction.CallbackContext obj)
        {
            _playerAnimation.Attack();
            //_playerAnimation.DeAttack();
        }
        private void HandleMovement()
        {
            if (GameSession.Instance.IsDied==false)
            {
                Move();
                Rotate();
            }
        
        }
        //Move
        private void Move()
        {
            var movementInput = _gameInput.Player.Movement.ReadValue<Vector2>();
            _characterMovement.MovementInput = movementInput;
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


