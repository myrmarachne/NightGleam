using UnityEngine;

public class GameController : MonoBehaviour {
    public Game game = Game.GetInstance();
	
	void Update () {
        if (game.State == GameState.Playing && game.Player.Lifes == 0) {
            game.State = GameState.GameOver;
            Debug.Log("GAME OVER");
        }
		if (Input.GetKey("escape")) {
			Debug.Log("QUIT");
			Application.Quit();
		}
	}
}
