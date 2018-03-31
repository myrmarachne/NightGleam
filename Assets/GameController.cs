using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    public static GameController gameController;

    public bool gameOver = false;
    public int lifes;

    void Awake () {
		if (gameController == null) {
            gameController = this;
        } else if (gameController != this) {
            Destroy(this.gameObject);
        }

        lifes = 3;
	}
	
	void Update () {
	
	}

    public void PlayerDeath() {

        Debug.Log("GAME OVER");
        gameOver = true;

    }
}
