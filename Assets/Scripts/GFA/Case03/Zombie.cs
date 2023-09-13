using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private Rigidbody[] _rigidBodies;
    private CharacterController _characterController;
    private Animator _animators;
    private ZombieState _currentState;
    [SerializeField] private GameObject _player;
    [SerializeField] private float _speed;
    Vector3 _dir2;

    //[SerializeField] private Camera _camera;
    private void Awake()
    {
        //_camera = Camera.main;
        _rigidBodies = GetComponentsInChildren<Rigidbody>();
        _characterController = GetComponent<CharacterController>();
        _animators = GetComponent<Animator>();
        OnDisableRagdoll();
        _currentState = ZombieState.Walking;
    }

    private void Update()
    {
        switch (_currentState)
        {
            case ZombieState.Walking:
                ZombieWalking();
                break;
            case ZombieState.Ragdoll:
                ZombieRagdoll();
                break; ;

        }


    }

    private void OnEnableRagdoll()
    {
        foreach (Rigidbody rigid in _rigidBodies)
        {
            rigid.isKinematic = false;
        }
        _characterController.enabled = false;
        _animators.enabled = false;
    }
    private void OnDisableRagdoll()
    {
        foreach (Rigidbody rigid in _rigidBodies)
        {
            rigid.isKinematic = true;
        }
        _characterController.enabled = true;
        _animators.enabled = true;
    }

    private enum ZombieState
    {
        Walking,
        Ragdoll
    }

    private void ZombieWalking()
    {
        Vector3 dir = _player.transform.position - transform.position;
        dir.y = 0;
        _dir2 = dir.normalized;
        Quaternion toRotation = Quaternion.LookRotation(dir, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 45 * Time.deltaTime);
        if (_currentState==ZombieState.Walking)
        {
            Move();

        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnEnableRagdoll();
            _currentState = ZombieState.Ragdoll;

        }
    }
    private void ZombieRagdoll()
    {

        if (Input.GetKeyDown(KeyCode.W))
        {
            OnDisableRagdoll();
            _currentState = ZombieState.Walking;
        }
    }

    private void Move()
    {
        _characterController.SimpleMove(_dir2 * _speed);
    }


}
