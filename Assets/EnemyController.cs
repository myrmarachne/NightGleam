using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : PhysicsObject {

    public float speed = 10f;
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

        /* TODO: Parametr lifes z PlayerController możnaby wydzielić poza
         * ten obiekt, gdzieś na zewnątrz np do jakiegos game controllera,
         * gdzie bylaby informacja o poziomie itp
         * 
         * TODO: Stworzyć ogólny 'game controller' */

        if (collision.collider.name == "Player") {
            PlayerController playerController = FindObjectOfType<PlayerController>();
            playerController.lifes--;
            if (playerController.lifes == 0) {
                Debug.Log("Game Over");
            }

        }
    }
}
