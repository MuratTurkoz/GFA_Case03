using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerRagdoll : MonoBehaviour
{
    private Rigidbody[] _rigidBodies;
    private CharacterController _characterController;
    private Animator _animators;
    private ObjectState _currentState;
    private void Awake()
    {
        //_camera = Camera.main;
        _rigidBodies = GetComponentsInChildren<Rigidbody>();
        _characterController = GetComponent<CharacterController>();
        _animators = GetComponent<Animator>();
        OnDisableRagdoll();
        _currentState = ObjectState.Running;
    }

    private void Update()
    {

        switch (_currentState)
        {
            case ObjectState.Running:
                Running();
                break;
            case ObjectState.Ragdoll:
                Ragdoll();
                break; ;

        }
        //// �leri do�ru bir ���n ��kararak �arp��may� kontrol etmek i�in Raycast kullanabilirsiniz
        //Vector3 raycastOrigin = transform.position + _characterController.center; // I��n�n ba�lang�� noktas�

        //// ��te ���n� olu�turuyoruz
        //Ray ray = new Ray(raycastOrigin, transform.forward);

        //float maxDistance = 1.0f; // I��n ne kadar uza�a gidece�ini ayarlay�n

        //// I��n �arp��ma sonucunu depolayacak bir de�i�ken
        //RaycastHit hit;

        //// I��n �arp���yor mu diye kontrol ediyoruz
        //if (Physics.Raycast(ray, out hit, maxDistance))
        //{
        //    // I��n bir �eye �arpt�, �arp��ma hedefini hit nesnesi ile kontrol edebilirsiniz
        //    Debug.Log("Bir �eye �arpt�: " + hit.collider.name);
        //}

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
            if (!rigid.CompareTag("BaseBall"))
            {
                rigid.isKinematic = true;
            }
        }
        _characterController.enabled = true;
        _animators.enabled = true;
    }

    private enum ObjectState
    {
        Running,
        Ragdoll
    }

    private void Running()
    {
        OnDisableRagdoll();
        if (GameSession.Instance.IsDied == true)
        {
            _currentState = ObjectState.Ragdoll;
        }
    }
    private void Ragdoll()
    {
        OnEnableRagdoll();

    }



}
