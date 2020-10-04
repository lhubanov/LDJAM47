using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ExplosionInteraction : SphericalInteractionBase
{
    private float timeLeft;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if ( timeLeft < 0.0f )
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerStay( Collider other )
    {
        if ( other.attachedRigidbody )
        {
            
			other.attachedRigidbody.AddExplosionForce( 100.0f, transform.position, GetComponent<SphereCollider>().radius );
		}
    }
}
