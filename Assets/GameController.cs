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
        gameController.gameOver = true;

    }

    public void LifeChange(int n) {
        gameController.lifes += n;

        if (gameController.lifes == 0) {
           gameController.PlayerDeath();
        }

        Debug.Log("Lifes " + lifes.ToString());
    }
}
