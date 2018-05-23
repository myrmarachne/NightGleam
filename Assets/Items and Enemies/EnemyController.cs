using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : PhysicsObject {
	
    private const float SPEED = 1.2f;
	private float sign;

	protected override void Start() {
		base.Start();
		sign = 1f;
	}
    
    protected override void ComputeVelocity() {
		float xVelocityDelta = -rbody.velocity.x;
		if (IsGrounded()) {
			xVelocityDelta += SPEED * sign;
		}
		rbody.velocity += new Vector2(xVelocityDelta, 0);
    }

    protected void OnTriggerEnter2D(Collider2D collider) {
        if (collider.name == "npc_bound") {
            sign = sign * (-1f);
			// Move element to the opposite direction to avoid collision trigger after element turn
			transform.position += new Vector3(sign * SPEED, 0, 0);
			Vector3 scale = transform.localScale;
			scale.x *= -1;
			transform.localScale = scale;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.collider.name == "Spell(Clone)") {
            Destroy(this.gameObject);
        }

    }
}
