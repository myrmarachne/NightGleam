using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {
	private Game game = Game.GetInstance();

	protected void Start() {
		game.State = GameState.Playing;
	}

	void Update () {
		if (game.State == GameState.Playing && game.Player.Lifes == 0) {
			game.State = GameState.GameOver;
        }
	}
}
