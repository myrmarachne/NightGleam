using UnityEngine;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour {
	private Game game = Game.GetInstance();
	private CanvasGroup canvasGroup;

	protected virtual void Start() {
		canvasGroup = GetComponent<CanvasGroup> ();

		// Attach buttons click event listeners
		Button continueButton = GameObject.Find ("PauseMenuContinueButton").GetComponent<Button>();
		Button mainMenuButton = GameObject.Find ("PauseMenuMainMenuButton").GetComponent<Button>();
		continueButton.onClick.AddListener (Continue);
		mainMenuButton.onClick.AddListener (MainMenu);
	}

	protected virtual void Update(){
		// Handle Esc key down
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (game.State == GameState.Playing) 
				Pause ();
			else if (game.State == GameState.Paused)
				Continue ();
		}
	}

	private void Pause() { 
		// Pause game and show menu
		game.State = GameState.Paused;
		canvasGroup.alpha = 1.0f;
		canvasGroup.interactable = true;
		canvasGroup.blocksRaycasts = true;
		Time.timeScale = 0;
	}

	public void Continue() {
		// Continue game and hide menu
		game.State = GameState.Playing;
		canvasGroup.alpha = 0.0f;
		canvasGroup.interactable = false;
		canvasGroup.blocksRaycasts = false;
		Time.timeScale = 1;
	}

	public void MainMenu() {
		Time.timeScale = 1;
		GameManager.ShowMainMenu ();
	}
}
