using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour {

    /* Założenia:
     * (1) powinna isntniec maxymalna liczba odbic (ew timeout), po ktorych pileczka sie wypala +
     * (3) po uderzeniu w podloge (sufit) sie odbija, ale po uderzeniu w sciane dobrze, jakby sie nie odbijala, a znikała -> osobny poziom colliderów na sciany (?) +
     * (1) maxymalne bounciness + ograniczenia na velocity wzdluz x oraz wzdluz y +
     * (2) "wyskok" zaklecia - z wysokosci playera (wziac pod uwage np przy wyskoku itp) +
     * 
     * niech tuz przed kolizja z ziemia - stala szybkosc
     */

    private float maxVelocityX = 1;
    private float maxVelocityY = 1;

    private float collisionPadding = 0.01f;
    private int groundCollisionsCounter;
    private int maxGroundCollisions = 5;

    private float maxSeconds = 5f;

    private bool startVanishing;

    private Vector2 velocity;
    Rigidbody2D rbody;
    SpriteRenderer spriteRenderer;

    private void Awake() {
        velocity = Vector2.zero;
        groundCollisionsCounter = 0;

        spriteRenderer = GetComponent<SpriteRenderer>();
        Color textureColor = spriteRenderer.color;
        textureColor.a = 1f;
        spriteRenderer.color = textureColor;

        rbody = GetComponent<Rigidbody2D>();

    }

    private void Start () {
        

	}

    private void Update () {

	}

    private void FixedUpdate() {
        Color textureColor = spriteRenderer.color;
        textureColor.a -= (Time.deltaTime/maxSeconds);
        spriteRenderer.color = textureColor;

        if (textureColor.a <= 0f) {
            this.gameObject.SetActive(false);
            Destroy(this);
        }
    }

    public void Initialize(Vector2 velocity) {
        this.velocity = velocity;
        rbody.velocity = this.velocity;

    }

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.collider.name == "Ground") {
            /* Set velocity before collision with the floor */
            rbody.velocity = velocity;

        } else {
            /*
             * TODO: Handle collision with enemies             
             */
            this.gameObject.SetActive(false);
            Destroy(this);
        }
    }



}
