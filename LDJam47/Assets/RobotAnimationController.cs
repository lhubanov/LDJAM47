using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAnimationController : MonoBehaviour
{
    public Animator robot_animator;
    public Animator robot_wheel_animator;
    Vector3 previous_position;
    
    // Start is called before the first frame update
    void Start()
    {
        previous_position = transform.position;
    }

    void FixedUpdate()
    {
        float speed = Vector3.Distance(transform.position, previous_position);
        previous_position = transform.position;

        if ( speed > 0.1f)
        {
            robot_animator.SetBool("Moving", true);
            robot_wheel_animator.SetBool("Moving", true);
        }
        else
        {
            robot_animator.SetBool("Moving", false);
            robot_wheel_animator.SetBool("Moving", false);
        }
    }

}
