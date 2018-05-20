using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour {

    /* TODO:
     * ustalić szybkości pocisku tak, aby mógł przeskoczyć NPC
     */

	/* Maximum time the spell exists */
    private float maxSeconds = 5f;

	/* Current spell velocity */
    private Vector2 velocity;
	private float velocity_x;

	/* Used for light opacity changes */
	private Light spellLight;

    Rigidbody2D rbody;
    SpriteRenderer spriteRenderer;



    private void Awake() {
        velocity = Vector2.zero;

        spellLight = GetComponent<Light>();
        spellLight.range = 0.5f;

        spriteRenderer = GetComponent<SpriteRenderer>();

        Color textureColor = spriteRenderer.color;
        textureColor.a = 1f;
        spriteRenderer.color = textureColor;

        rbody = GetComponent<Rigidbody2D>();

    }


    private void FixedUpdate() {

        Color textureColor = spriteRenderer.color;
        textureColor.a -= (Time.deltaTime/maxSeconds);
        spriteRenderer.color = textureColor;

        spellLight.range -= (Time.deltaTime / (2*maxSeconds));


        if (textureColor.a <= 0f) {
            Destroy(this.gameObject);
        }
			
    }

    public void Initialize(Vector2 velocity) {
        this.velocity = velocity;

		/* Initial velocity is one half smaller */
        rbody.velocity = this.velocity * 0.5f;

    }

	private void OnCollisionEnter2D(Collision2D collision) {
		if (collision.collider.name != "Ground") {

			/* Handle collision with Enemies */
			Destroy(this.gameObject);


		} else {

			/* Check the normal vector to the surface */
			ContactPoint2D[] contactPoints = new ContactPoint2D[16];
			int numberOfContactPoints = collision.GetContacts(contactPoints);

			bool anihilate = false;
			Vector2 normal = Vector2.zero;

			for (int i = 0; i < numberOfContactPoints; i++) {
				normal = contactPoints[i].normal;

				/* Check if the normal vector is horizontal */
				if (normal.y == 0 && normal.x != 0) {
					anihilate = true;
					break;
				}
				/* Check if the normal vector is a vertical vector
                 * pointing to ground [down] */
				else if (normal.y < 0 && normal.x == 0) {
					anihilate = true;
					break;
				}
			}


			if (anihilate) {
				Destroy(this.gameObject);
			} else {
				/* Set velocity before collision with the floor */
				rbody.velocity = velocity;
			}


		}
	}



}
