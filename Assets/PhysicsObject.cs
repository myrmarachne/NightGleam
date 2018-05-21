using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour {
    protected Rigidbody2D rbody;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected ContactFilter2D contactFilter;

    private void OnEnable() {
        rbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start () {
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
        ComputeVelocity();
	}

    protected virtual void ComputeVelocity() { }

	protected bool IsGrounded() {
		float elementHeight = GetComponent<Collider2D>().bounds.extents.y;
		int hitsUnder = rbody.Cast(-Vector2.up, contactFilter, hitBuffer, elementHeight);
		for (int i = 0; i < hitsUnder; i++) {
			if (hitBuffer[i].distance <= elementHeight) {
				return true;
			}
		}
		return false;
	}
}
