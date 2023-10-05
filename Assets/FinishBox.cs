using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishBox : MonoBehaviour
{
    BoxCollider _boxCollider;
    // Start is called before the first frame update
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        GameSession.Instance.Health = 0;
        GameSession.Instance.IsDied = true;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
