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
            Debug.Log(sign);
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
            Debug.Log("hello");
        }
    }
}
