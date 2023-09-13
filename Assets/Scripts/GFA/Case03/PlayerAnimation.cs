using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] Animator _anim;

    public void Attack()
    {
        //_anim.SetBool("IsRun",false);
    }
    public void DeAttack() {

        //_anim.SetBool("IsRun", true);
    }
 
}
