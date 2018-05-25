using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBubbleController : MonoBehaviour {


	private float maxSeconds = 2f;

	/* Current spell velocity */
	public float y;

	Rigidbody2D rbody;
	SpriteRenderer spriteRenderer;

	private void Awake() {

		spriteRenderer = GetComponent<SpriteRenderer>();

		Color textureColor = spriteRenderer.color;
		textureColor.a = 1f;
		spriteRenderer.color = textureColor;

		rbody = GetComponent<Rigidbody2D>();
		rbody.velocity = new Vector2 (0, y);
	}


	private void FixedUpdate() {

		Color textureColor = spriteRenderer.color;
		textureColor.a -= (Time.deltaTime/maxSeconds);
		spriteRenderer.color = textureColor;

		if (textureColor.a <= 0f) {
			Destroy(this.gameObject);
		}
		rbody.velocity = new Vector2 (0, y);

	}

}
