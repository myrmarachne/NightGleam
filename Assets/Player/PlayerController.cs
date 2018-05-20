using System;
using UnityEngine;

public class PlayerController : PhysicsObject {

	/* Player speed */
    private const float JUMP_TAKE_OFF_SPEED = 10;
    private const float MAX_SPEED = 7;
	private JumpType jumpType;

	/* Player position */
	private bool playerTurnedRight = true;
	    
	/* Spell obects parameters */
	public GameObject spell;
    Transform spellPosition;
    private Vector2 spellVelocity = new Vector2(6,4);

	public bool changeCamera;

	protected override void Start () {
        base.Start();
        changeCamera = false;

        spellPosition = transform.Find("spellPosition");
	}
		
    protected override void ComputeVelocity() {
		float xVelocityDelta = -rbody.velocity.x + Input.GetAxis("Horizontal") * MAX_SPEED;
		rbody.velocity += new Vector2(xVelocityDelta, 0);
		handleJump();

        if ((rbody.velocity.x > 0 && !playerTurnedRight) || (rbody.velocity.x < 0 && playerTurnedRight)){
            /* Character is moving to right, while standing
             * turned to left or is moving to left, while
             * standing turned to right -> turn to the opposite 
             * direction */
            Turn();
        }

        if (Input.GetKeyDown(KeyCode.LeftControl)) {
            CastSpell();
        }

    }

    private void Turn() {
        /* Change the direction the player is currently facing */
        playerTurnedRight = !playerTurnedRight;

        /* Turn the player */
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void CastSpell() {
        SpellController castedSpell = Instantiate(spell, spellPosition.position, Quaternion.identity).GetComponent<SpellController>();

        Vector2 initialVelocity = spellVelocity;
        if (!playerTurnedRight) {
            initialVelocity.x *= (-1);
        }

        castedSpell.Initialize(initialVelocity);

    }

    void OnTriggerExit2D(Collider2D collider) {
        if (collider.gameObject.name == "FloorChange")
            changeCamera = true;
    }

	private void handleJump() {
		if (Input.GetButtonDown("Jump")) {
			float vy = rbody.velocity.y;
			if (IsGrounded()) {
				jumpType = JumpType.Normal;
				rbody.AddForce(new Vector2(0, JUMP_TAKE_OFF_SPEED), ForceMode2D.Impulse);
			}
			else if (jumpType == JumpType.Normal && vy > 0) {
				jumpType = JumpType.Double;
				// max +3 tiles, so double jump == 9 tiles high
				rbody.AddForce(new Vector2(0, JUMP_TAKE_OFF_SPEED * 0.3f), ForceMode2D.Impulse);
			}
		}
	}

	protected Boolean IsGrounded() {
		// if player moves vertically, then it cannot be grounded
		// (warning: it will not work with fast vertically moving platforms)
		if (Math.Abs(rbody.velocity.y) > 1) {
			return false;
		}
		return base.IsGrounded ();
	}
}
