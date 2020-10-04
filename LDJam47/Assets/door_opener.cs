using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door_opener : MonoBehaviour
{
    public GameObject left_door;
    public GameObject right_door;
    public Animator door_animator;

    bool test_bool = false;

    public void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if(!test_bool)
            {
                open_doors();
                test_bool = true;
            }
            else
            {
                close_doors();
                test_bool = false;
            }
          
        }
    }

    public void open_doors()
    {
        door_animator.SetBool("Open", true);
    }

    public void close_doors()
    {
        door_animator.SetBool("Open", false);
    }
}
