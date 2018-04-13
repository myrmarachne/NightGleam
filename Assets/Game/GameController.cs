using UnityEngine;
using UnityEngine.UI;



public class GameController : MonoBehaviour {
    private Game game = Game.GetInstance();

    private GameObject levelImage;
    private int level = 1;

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
