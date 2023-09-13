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
        public Vector2 MovementInput { get; set; }

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

                Vector3 direction = new Vector3();

                var movement = new Vector3(MovementInput.x, 0, MovementInput.y);
                transform.eulerAngles = new Vector3(0, Rotation);
                _characterController.SimpleMove(movement * _movementSpeed);
                MovementInput = Vector2.zero;
            }

        }
    }
}
