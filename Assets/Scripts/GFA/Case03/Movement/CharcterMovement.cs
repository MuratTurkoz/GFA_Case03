using GFA.Case03.Mediators;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GFA.Case03.Movement
{
    //Chracter Movement CharacterController yönetiyor...
    [RequireComponent(typeof(CharacterController))]
    public class CharacterMovement : MonoBehaviour
    {
        private CharacterController _characterController;
        public CharacterController CharacterControllerOld { get => _characterController;set { _characterController = value; } }
        [SerializeField]PlayerMediator _playerMediator;
        [SerializeField]
        private float playerSpeed = 0.2f;
        [SerializeField]
        private float playerRunSpeed = 4f;
        public Vector2 MovementInput { get; set; }
        [SerializeField]
        private float jumpHeight = 1.0f;
        [SerializeField]
        private float gravityValue = -9.81f;
        private Vector3 playerVelocity;
        private bool groundedPlayer;
        private Transform cameraTransform;


        [SerializeField]
        private float _movementSpeed = 4;
        public float MovementSpeed
        {
            get => _movementSpeed;
            set => _movementSpeed = value;
        }

        public float Rotation { get; set; }

        public Vector3 Velocity => _characterController.velocity;


        private void Awake()
        {
            cameraTransform = Camera.main.transform;

            _characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (GameSession.Instance.IsDied == false)
            {

                Move();
            }
        }
        private void Move()
        {
            if (_characterController != null)
            {
                //groundedPlayer = _characterController.isGrounded;
                //Vector3 direction = new Vector3();

                //var movement = new Vector3(MovementInput.x, 0, MovementInput.y);
                //transform.eulerAngles = new Vector3(0, Rotation);
                //_characterController.SimpleMove(movement * _movementSpeed);

                //MovementInput = Vector2.zero;
                //if (_playerMediator.IsJumped && groundedPlayer)
                //{
                //    playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                //}
                //playerVelocity.y += gravityValue * Time.deltaTime;
                //_characterController.Move(playerVelocity * Time.deltaTime);


                //Debug.Log(_playerMediator.IsJumped);
                groundedPlayer = _characterController.isGrounded;

                if (groundedPlayer && playerVelocity.y < 0)
                {
                    playerVelocity.y = 0f;
                }

                Vector2 movement = _playerMediator.Movement;
                Vector3 move = new Vector3(movement.x, 0, movement.y);
                move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
                //Debug.Log(cameraTransform.rotation.x);
                transform.rotation = Quaternion.Euler(cameraTransform.rotation.x, 0, 0);
                move.y = 0f;
                if (_playerMediator.IsRun == true)
                {
                    _characterController.Move(move * Time.deltaTime * playerRunSpeed);
                }
                _characterController.Move(move * Time.deltaTime * playerSpeed);

                if (move != Vector3.zero)
                {
                    gameObject.transform.forward = move;
                }

                if (_playerMediator.IsJumped && groundedPlayer)
                {
                    playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
                }
                playerVelocity.y += gravityValue * Time.deltaTime;
                _characterController.Move(playerVelocity * Time.deltaTime);
            }

        }
    }
}
