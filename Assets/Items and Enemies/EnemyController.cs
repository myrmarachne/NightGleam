using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : PhysicsObject {
    private const float SPEED = 1.5f;

    private Game game = Game.GetInstance();
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
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.collider.name == "Player") {
            game.Player.Lifes--;
        } else if (collision.collider.name == "Spell(Clone)") {
            Destroy(this.gameObject);
        }

    }
}
