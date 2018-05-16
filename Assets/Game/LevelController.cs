using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {

	private Game game = Game.GetInstance();
	public static LevelController instance = null;

	public Transform gameOverMenuCanvas;

	protected void Awake() {

		/* Singleton pattern */
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
	}

	protected void Start() {
		game.State = GameState.Playing;
		if (gameOverMenuCanvas.gameObject.activeInHierarchy) {
			gameOverMenuCanvas.gameObject.SetActive (false);
		}
	}

	void Update () {

	}

	public void LoadMainMenu(){
		Time.timeScale = 1;
		SceneManager.LoadScene ("MainMenu");
	}

	public void LoadGameOverMenu(){
		game.State = GameState.GameOver;
		gameOverMenuCanvas.gameObject.SetActive (true);
		Time.timeScale = 0;
	}



}
