using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ExplosionInteraction : SphericalInteractionBase
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay( Collider other )
    {
        if ( other.attachedRigidbody )
        {
            
			other.attachedRigidbody.AddExplosionForce( 100.0f, transform.position, GetComponent<SphereCollider>().radius );
		}
    }
}
