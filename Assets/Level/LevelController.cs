using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {
	private Game game = Game.GetInstance();

	private float lifes;

	protected void Start() {
		game.State = GameState.Playing;
		lifes = game.Player.getLifes();
	}

	void Update () {
		if (game.Player.getLifes() != lifes) {
			UpdateLifes ();
		}

		if (game.State == GameState.Playing && game.Player.getLifes() == 0) {
			game.State = GameState.GameOver;
        }
	}

	private void colorNthHeart(int n, Color color){
		// Change the color of n-th heart to color
		string heartString = "Life(" + n.ToString() + ")";
		SpriteRenderer spriteRenderer = GameObject.Find (heartString).GetComponent<SpriteRenderer> ();
		spriteRenderer.color = color;

	}

	private void UpdateLifes(){
		// Change the graphics of the hearts

		int i;

		for (i = 1; i <= Mathf.Floor(game.Player.getLifes()); i++){
			colorNthHeart (i, Color.white);
		}
			
		if (Mathf.Floor (game.Player.getLifes()) != game.Player.getLifes()) {
			// Color next heart grey
			colorNthHeart (i, Color.grey);
			i++;
		}

		for (; i <= game.maxLifes; i++) {
			colorNthHeart (i, Color.black);

		}

		this.lifes = game.Player.getLifes();
	}

}
