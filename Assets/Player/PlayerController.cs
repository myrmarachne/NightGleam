using System;
using UnityEngine;

public class PlayerController : PhysicsObject {

	private Game game = Game.GetInstance();

	/* Player speed */
    private const float JUMP_TAKE_OFF_SPEED = 10;
    private const float MAX_SPEED = 7;
	private JumpType jumpType;

	/* Player position */
	private bool playerTurnedRight = true;
	    
	/* Spell obects parameters */
	public GameObject spell;
    Transform spellPosition;
    private Vector2 spellVelocity = new Vector2(6,9);

	/* After collision with NPC */
	private float stopTime = 0.1f;
	private float blinkingTime = 2.0f;

	private float stopTimeLeft;
	private float blinkingTimeLeft;
	private bool ignoreNPCCollisions = false;

	private float currentBlink;

	SpriteRenderer spriteRenderer;
	Animator animator;

	public bool changeCamera;

	protected void Awake(){
		Physics2D.IgnoreLayerCollision (8, 9, false);

		spriteRenderer = GetComponent<SpriteRenderer>();
		animator = GetComponent<Animator> ();

		Color textureColor = spriteRenderer.color;
		textureColor.a = 1f;
		spriteRenderer.color = textureColor;
	}

	protected override void Start () {
        base.Start();
        changeCamera = false;

        spellPosition = transform.Find("spellPosition");
	}

	protected override void Update(){
		
		if (stopTimeLeft <= 0) {
			base.Update ();

		} else {
			stopTimeLeft -= Time.deltaTime;
		}

		if (blinkingTimeLeft > 0) {

			blinkingTimeLeft -= Time.deltaTime;
			currentBlink += Time.deltaTime; 

			if (currentBlink > 0.3f) {
				currentBlink = 0;

				Color textureColor = spriteRenderer.color;
				textureColor.a *= -1;
				textureColor.a += 1.5f;
				spriteRenderer.color = textureColor;
			}

			if (blinkingTimeLeft <= 0) {
				Physics2D.IgnoreLayerCollision (8, 9, false);

				Color textureColor = spriteRenderer.color;
				textureColor.a = 1f;
				spriteRenderer.color = textureColor;
				ignoreNPCCollisions = false;
			}

		}
	}

	protected void FixedUpdate(){
		animator.SetFloat ("velocity", Mathf.Abs (rbody.velocity.x));
		animator.SetBool ("IsGrounded", IsGrounded ());
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

	private void handleJump() {
		if (Input.GetButtonDown("Jump")) {


			float vy = rbody.velocity.y;
			if (IsGrounded()) {
				jumpType = JumpType.Normal;
				rbody.AddForce(new Vector2(0, JUMP_TAKE_OFF_SPEED), ForceMode2D.Impulse);

				animator.SetTrigger ("jump");

			}
			else if (jumpType == JumpType.Normal && vy > 0) {
				jumpType = JumpType.Double;
				// max +3 tiles, so double jump == 9 tiles high
				rbody.AddForce(new Vector2(0, JUMP_TAKE_OFF_SPEED * 0.3f), ForceMode2D.Impulse);
			}
		}
	}

	protected bool IsGrounded() {
		// if player moves vertically, then it cannot be grounded
		// (warning: it will not work with fast vertically moving platforms)
		if (Math.Abs(rbody.velocity.y) > 1) {
			return false;
		}
		return base.IsGrounded ();
	}


	private void CastSpell() {
		if (game.State == GameState.Playing && Time.timeScale != 0) {
			SpellController castedSpell = Instantiate (spell, spellPosition.position, Quaternion.identity).GetComponent<SpellController> ();

			Vector2 initialVelocity = spellVelocity;
			if (!playerTurnedRight) {
				initialVelocity.x *= (-1);
			}

			castedSpell.Initialize (initialVelocity);
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
			if (collider.gameObject.name == "FloorChange")
					changeCamera = true;
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject.name == "Elixir") {
			game.Player.SetLifes (game.Player.GetLifes () + Item.ElixirImpact ());
		} else if (collider.gameObject.name == "Door") {
			Animator doorAnimator = collider.GetComponent<Animator> ();
			doorAnimator.SetTrigger ("open");

		}
	}



	private void OnCollisionEnter2D (Collision2D collision){
		if (collision.collider.name == "NPC" && !ignoreNPCCollisions) {
			
			game.Player.SetLifes(game.Player.GetLifes() - 1);

			Physics2D.IgnoreLayerCollision (8, 9, true);

			stopTimeLeft = stopTime;
			blinkingTimeLeft = blinkingTime;
			currentBlink = 0;
			ignoreNPCCollisions = true;
		}
	}
}
