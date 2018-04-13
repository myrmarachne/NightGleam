using System;
using UnityEngine;

public class PlayerController : PhysicsObject {
    private const float JUMP_TAKE_OFF_SPEED = 8;
    private const float MAX_SPEED = 7;

	public bool changeCamera;
	private JumpType jumpType;
    private Player player = Game.GetInstance().Player;
    public GameObject spell;
    Transform spellPosition;
    private Vector2 spellVelocity = new Vector2(2,3);

    private bool playerTurnedRight = true;

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
			if (IsGrounded()) {
				jumpType = JumpType.Normal;
				rbody.AddForce(new Vector2(0, JUMP_TAKE_OFF_SPEED), ForceMode2D.Impulse);
			}
			else if (jumpType == JumpType.Normal) {
				jumpType = JumpType.Double;
				rbody.AddForce(new Vector2(0, JUMP_TAKE_OFF_SPEED), ForceMode2D.Impulse);
			}
		}
	}
}
