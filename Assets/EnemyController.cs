using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : PhysicsObject {

    private float speed = 4f;
    private Game game = Game.GetInstance();
    private Vector2 move;
    private float sign;
    
    protected override void ComputeVelocity() {
        if (grounded) {
            move = new Vector2(sign * groundNormal.y, groundNormal.x);
        }
        targetVelocity = move * speed;
    }

    protected override void Start() {
        base.Start();
        move = Vector2.zero;
        sign = 1f;
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
