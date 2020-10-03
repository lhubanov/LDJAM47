using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isaacs_shit_test_movement : MonoBehaviour
{
    public Transform ball;
    public CharacterController controller;
    public float speed = 12f;

    // Update is called once per frame
    void Update()
    {
        float horiz = Input.GetAxisRaw("Horizontal");
        float verti = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horiz, 0f, verti).normalized;

        if( direction.magnitude >= 0.1f)
        {
            controller.Move(direction * speed * Time.deltaTime);
            ball.Rotate(0, 225 * Time.deltaTime, 0); //rotates 50 degrees per second around z axis
        }
    }
}
