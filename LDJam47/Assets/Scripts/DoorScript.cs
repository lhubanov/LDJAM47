using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DoorScript : MonoBehaviour
{
    enum DoorState
    {
        CLOSED,
        OPEN
    }

    Animator animator;
    DoorState state = DoorState.CLOSED;

    private void OnTriggerStay(Collider other)
    {
        animator.SetTrigger("Open");
    }
    private void OnTriggerEnter(Collider other)
    {
        if( state == DoorState.CLOSED)
        {

            animator.SetTrigger("Open");
            state = DoorState.OPEN;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (state == DoorState.OPEN)
        {
            animator.SetTrigger("Close");
            state = DoorState.CLOSED;
        }
    }
}
