using System;
using UnityEngine;

public class PlayerController : PhysicsObject {

    public float jumpTakeOffSpeed = 14;
    public float maxSpeed = 7;
	private float halfJumpThreshold = 12;
    private Player player = Game.GetInstance().Player;

    public bool changeCamera;

	protected override void Start () {
        base.Start();
        changeCamera = false;
	}
		
    protected override void ComputeVelocity() {

        /* The purpose of this function is to check
         * for incoming inputs and compute based on them
         * update the value of the targetVelocity of the 
         * main character */

		targetVelocity = Vector2.zero;

        /* Get the value of horizontal movement 
         * movement.x value is in range [-1, 1] */

		targetVelocity.x = Input.GetAxis("Horizontal") * maxSpeed;

		handleJump();
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.name == "FloorChange")
            changeCamera = true;
    }

	private void handleJump() {
		// End of the jump detected
		if (player.Jump != JumpType.NoJump && grounded) {
			player.Jump = JumpType.NoJump;
		}

		if (Input.GetButtonDown("Jump") || (Input.GetButton("Jump") && grounded)) {
			// "Jump" button was pressed while grounded - start jump
			if (grounded) {
				player.Jump = JumpType.Normal;
				targetVelocity.y = jumpTakeOffSpeed;
			}
			// "Jump" was pressed in the second part of the earlier jump - trigger double jump
			else if (Math.Abs(velocity.y) <= halfJumpThreshold && player.Jump == JumpType.Normal) {
				player.Jump = JumpType.Double;
				targetVelocity.y = jumpTakeOffSpeed;
				if (velocity.y < 0) {
					targetVelocity.y -= velocity.y;
				}
			}
		}
		// "Jump"" button released
		else if (Input.GetButtonUp("Jump")) {
			// Recognize half jump
			if (velocity.y > 0 && velocity.y > halfJumpThreshold && player.Jump == JumpType.Normal) {
				player.Jump = JumpType.Half;
			}
		}
		// If half jump and player already passed upper limit of the jump half - get him down.
		if (player.Jump == JumpType.Half && velocity.y > 0 && velocity.y <= halfJumpThreshold) {
			targetVelocity.y = -velocity.y * .5f;
		}
	}
}
