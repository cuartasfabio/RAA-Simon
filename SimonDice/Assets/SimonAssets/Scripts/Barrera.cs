using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrera : MonoBehaviour
{
    
    private Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void OpenDoor() {
        _animator.SetBool("abierta", true);
    }

    public void CloseDoor() {
        _animator.SetBool("abierta", false);
    }


}
