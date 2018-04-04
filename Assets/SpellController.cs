using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour {

    /* Założenia:
     * (1) powinna isntniec maxymalna liczba odbic (ew timeout), po ktorych pileczka sie wypala
     * (3) po uderzeniu w podloge (sufit) sie odbija, ale po uderzeniu w sciane dobrze, jakby sie nie odbijala, a znikała -> osobny poziom colliderów na sciany (?)
     * (1) maxymalne bounciness + ograniczenia na velocity wzdluz x oraz wzdluz y
     * (2) "wyskok" zaklecia - z wysokosci playera (wziac pod uwage np przy wyskoku itp)
     * 
     */

    private float maxVelocityX = 1;
    private float maxVelocityY = 1;

    private float collisionPadding = 0.01f;

    private Vector2 velocity = new Vector2(1,0);
    Rigidbody2D rbody;

    private void Awake() {
        rbody = GetComponent<Rigidbody2D>();
        rbody.velocity = velocity;
    }

    private void Start () {
        

	}

    private void Update () {
	}

    private void FixedUpdate() {

       

    }

}
