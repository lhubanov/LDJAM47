using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class DragInteraction : MonoBehaviour
{
    // Start is called before the first frame update
    private Vector3 pickupScreenPoint;
    private Vector3 offsetFromMousePointer;
    private Vector3 pickupOffset;
    private float previousWorldY;

    [SerializeField]
    private bool addAsTargetOnDrop = false;

    public float heightOfPickup = 0.5f;

    public GameObject effect;

    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnMouseDown()
    {
        pickupScreenPoint = Camera.main.WorldToScreenPoint( transform.position );
        offsetFromMousePointer = transform.position - Camera.main.ScreenToWorldPoint( new Vector3( Input.mousePosition.x, Input.mousePosition.y, pickupScreenPoint.z ) );
        pickupOffset = Vector3.up * heightOfPickup;

        previousWorldY = transform.position.y;

        // rb.isKinematic = true;
        // rb.detectCollisions = false;
    }

    void OnMouseDrag()
    {
        Vector3 currentScreenPoint = new Vector3( Input.mousePosition.x, Input.mousePosition.y, pickupScreenPoint.z );

        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint( currentScreenPoint );
        mouseWorldPoint.y = previousWorldY;
        Vector3 currentPosition = mouseWorldPoint + offsetFromMousePointer + pickupOffset;

        rb.AddForce( currentPosition - transform.position );

        Vector3 overrideHeight = transform.position;
        overrideHeight.y = pickupOffset.y + previousWorldY;
        transform.position = overrideHeight;
    }

    void OnMouseUp()
    {
        if ( effect )
        {
            GameObject effectInstance = Instantiate( effect, transform.position, Quaternion.identity );
        }

        if ( addAsTargetOnDrop )
        {
            PlayerController controller = FindObjectOfType<PlayerController>();
            if (controller)
            {
                controller.InsertObjective(transform.position);
            }
        }
    }
}