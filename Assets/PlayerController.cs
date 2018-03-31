using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : PhysicsObject {

    public float jumpTakeOffSpeed = 7;
    public float maxSpeed = 7;

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

        Vector2 movement = Vector2.zero;

        /* Get the value of horizontal movement 
         * movement.x value is in range [-1, 1] */

        movement.x = Input.GetAxis("Horizontal");

        /* Check if the jump button is pressed and if the
         * character is currently staying on the ground.
         * 
         * 
         * TODO: Podwojny skok lub skok w powietrzu */

        if (Input.GetButtonDown("Jump") && grounded) {

            velocity.y = jumpTakeOffSpeed;

        } else if (Input.GetButtonUp("Jump")) {

            // Reduce the velocity after releasing the jump button
            if (velocity.y > 0)
                velocity.y = velocity.y * .5f;
        }

        targetVelocity = movement * maxSpeed;
    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.name == "FloorChange")
            changeCamera = true;
    }



}
