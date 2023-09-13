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
        //// Ýleri doðru bir ýþýn çýkararak çarpýþmayý kontrol etmek için Raycast kullanabilirsiniz
        //Vector3 raycastOrigin = transform.position + _characterController.center; // Iþýnýn baþlangýç noktasý

        //// Ýþte ýþýný oluþturuyoruz
        //Ray ray = new Ray(raycastOrigin, transform.forward);

        //float maxDistance = 1.0f; // Iþýn ne kadar uzaða gideceðini ayarlayýn

        //// Iþýn çarpýþma sonucunu depolayacak bir deðiþken
        //RaycastHit hit;

        //// Iþýn çarpýþýyor mu diye kontrol ediyoruz
        //if (Physics.Raycast(ray, out hit, maxDistance))
        //{
        //    // Iþýn bir þeye çarptý, çarpýþma hedefini hit nesnesi ile kontrol edebilirsiniz
        //    Debug.Log("Bir þeye çarptý: " + hit.collider.name);
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
