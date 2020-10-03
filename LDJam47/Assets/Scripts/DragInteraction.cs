using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class DragInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 screenPoint;
    private Vector3 offsetFromMousePointer;
    private Vector3 pickupOffset;
    private float worldHeight;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnMouseDown()
    {
        screenPoint = Camera.main.WorldToScreenPoint( transform.position );
        offsetFromMousePointer = transform.position - Camera.main.ScreenToWorldPoint( new Vector3( Input.mousePosition.x, Input.mousePosition.y, screenPoint.z ) );
        pickupOffset = Vector3.up * 0.2f;

        worldHeight = transform.position.y;
    }

    void OnMouseDrag()
    {
        Vector3 currentScreenPoint = new Vector3( Input.mousePosition.x, Input.mousePosition.y, screenPoint.z );

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint( currentScreenPoint );
        mouseWorld.y = worldHeight;
        Vector3 currentPosition = mouseWorld + offsetFromMousePointer + pickupOffset;
        transform.position = currentPosition;
    }

    void OnMouseUp()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if ( rb )
        {
            rb.velocity = Vector3.zero;
        }
    }
}