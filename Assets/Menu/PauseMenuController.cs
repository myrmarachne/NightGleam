using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour {

	public Transform pauseMenuCanvas;

	private Game game = Game.GetInstance();

	protected virtual void Start() {
		if (pauseMenuCanvas.gameObject.activeInHierarchy) {
			pauseMenuCanvas.gameObject.SetActive (false);
		}

	}

	protected virtual void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (game.State == GameState.Playing) 
				Pause ();
			 else if (game.State == GameState.Paused)
				Continue ();
		}
	}

	private void Pause(){
		game.State = GameState.Paused;
		pauseMenuCanvas.gameObject.SetActive (true);
		Time.timeScale = 0;

	}

	public void Continue(){
		game.State = GameState.Playing;
		pauseMenuCanvas.gameObject.SetActive (false);
		Time.timeScale = 1;
	}



}
