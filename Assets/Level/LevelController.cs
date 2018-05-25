using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {
	private Game game = Game.GetInstance();

	private float lifes;

	protected void Awake(){
		UpdateLifes ();
	}

	protected void Start() {
		game.State = GameState.Playing;
		lifes = game.Player.getLifes();

		Physics2D.IgnoreLayerCollision (8, 9, false);

		UpdateLevelIndicator ();
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
		Image spriteRenderer = GameObject.Find (heartString).GetComponent<Image> ();
		spriteRenderer.color = color;

	}

	private void UpdateLifes(){
		// Change the graphics of the hearts

		int i;

		for (i = 1; i <= Mathf.Floor(game.Player.getLifes()); i++){
			colorNthHeart (i, new Color (1, 1, 1, 0.5f));
		}
			
		if (Mathf.Floor (game.Player.getLifes()) != game.Player.getLifes()) {
			// Color next heart grey
			colorNthHeart (i, new Color (1, 1, 1, 0.5f));
			i++;
		}

		for (; i <= game.maxLifes; i++) {
			colorNthHeart (i, new Color (1, 1, 1, 0.15f));

		}

		this.lifes = game.Player.getLifes();
	}

	private void UpdateLevelIndicator() {
		GameObject indicator = GameObject.Find ("LevelIndicatorText");
		if (indicator) {
			indicator.GetComponent<Text> ().text = "Level " + game.Level;
		}
	}
}
