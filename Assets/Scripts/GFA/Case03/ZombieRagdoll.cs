using GFA.Case03.MatchSystem;
using GFA.Case03.Mediators;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GFA.Case03.Mediators.PlayerMediator;

public class ZombieRagdoll : MonoBehaviour
{
    private Rigidbody[] _rigidBodies;
    private CharacterController _characterController;
    private Animator _animators;
    private ObjectState _currentState;
    [SerializeField] private GameObject _player;
    [SerializeField] private float _speed;
    [SerializeField] private MatchInstance _matchInstance;
    Vector3 _dir2;
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
                ZombieWalking();
                Attack();
                break;
            case ObjectState.Ragdoll:
                Ragdoll();
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
        StartCoroutine(nameof(Die));
    }
    private void OnDisableRagdoll()
    {
        //_currentState = ObjectState.Running;
        foreach (Rigidbody rigid in _rigidBodies)
        {
            rigid.isKinematic = true;
        }
        _characterController.enabled = true;
        _animators.enabled = true;
    }

    private enum ObjectState
    {
        Running,
        Ragdoll
    }

    private void Ragdoll()
    {
        OnEnableRagdoll();
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Hi");

        if (other.CompareTag("BaseBall") && !other.CompareTag("Player"))
        {
            _currentState = ObjectState.Ragdoll;
            //Ragdoll();
        }
    }
    private IEnumerator Die()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
    private void ZombieWalking()
    {
        if (GameSession.Instance.IsDied == false)
        {
            Vector3 dir = _matchInstance.Player.transform.position - transform.position;
            dir.y = 0;
            _dir2 = dir.normalized;
            Quaternion toRotation = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, 45 * Time.deltaTime);
            if (_currentState == ObjectState.Running)
            {
                Move();

            }
        }
  

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    OnEnableRagdoll();
        //    _currentState = ObjectState.Ragdoll;

        //}
    }
    private void Move()
    {
        _characterController.SimpleMove(_dir2 * _speed);
    }
    private void Attack()
    {
        // Ýleri doðru bir ýþýn çýkararak çarpýþmayý kontrol etmek için Raycast kullanabilirsiniz
        Vector3 raycastOrigin = transform.position + _characterController.center; // Iþýnýn baþlangýç noktasý

        // Ýþte ýþýný oluþturuyoruz
        Ray ray = new Ray(raycastOrigin, transform.forward);

        float maxDistance = 1f; // Iþýn ne kadar uzaða gideceðini ayarlayýn

        // Iþýn çarpýþma sonucunu depolayacak bir deðiþken
        RaycastHit hit;

        // Iþýn çarpýþýyor mu diye kontrol ediyoruz
        if (Physics.Raycast(ray, out hit, maxDistance))
        {
            if (hit.collider.CompareTag("Player"))
            {
                GameSession.Instance.Health -= 0.1f * Time.deltaTime;
     
                //_matchInstance.Player.GetComponent<PlayerMediator>()
                //Debug.Log(PlayerMediator.PlayerInstance.Health);
                // Iþýn bir þeye çarptý, çarpýþma hedefini hit nesnesi ile kontrol edebilirsiniz
                Debug.Log(GameSession.Instance.Health);
                Debug.Log("Bir þeye çarptý: " + hit.collider.name);
            }
        }
    }




}
