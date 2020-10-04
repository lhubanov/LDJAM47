using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallExplosionOnClick : MonoBehaviour
{
    [SerializeField]
    private float explosionForce = 10.0f;

    [SerializeField]
    private float explosionRadius = 10.0f;

    [SerializeField]
    private float explosionUpwardsModifier = 10.0f;


    [SerializeField]
    private Rigidbody rigidbody;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
        if(rigidbody)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;

            if ( Physics.Raycast(ray, out hitInfo) )
            {
                rigidbody.AddExplosionForce(explosionForce, hitInfo.point, explosionRadius, explosionUpwardsModifier);
            }
        }
    }
}
