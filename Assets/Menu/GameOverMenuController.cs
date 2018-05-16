using UnityEngine;
using UnityEngine.UI;

public class GameOverMenuController : MonoBehaviour {
	private Game game = Game.GetInstance();
	private CanvasGroup canvasGroup;
	private bool isMenuVisible = false;

	protected virtual void Start() {
		canvasGroup = GetComponent<CanvasGroup> ();

		// Attach buttons click event listeners
		Button startNewGameButton = GameObject.Find ("GameOverMenuStartNewGameButton").GetComponent<Button>();
		Button mainMenuButton = GameObject.Find ("GameOverMenuMainMenuButton").GetComponent<Button>();
		startNewGameButton.onClick.AddListener (StartNewGame);
		mainMenuButton.onClick.AddListener (MainMenu);
	}

	protected virtual void Update(){
		if (!isMenuVisible) {
			if (game.State == GameState.GameOver) {
				ShowMenu ();
			}
		}
		else {
			// Handle Esc key down
			if (Input.GetKeyDown (KeyCode.Escape)) {
				MainMenu ();
			}
		}
	}

	private void ShowMenu() { 
		canvasGroup.alpha = 1.0f;
		canvasGroup.interactable = true;
		canvasGroup.blocksRaycasts = true;
		Time.timeScale = 0;
		isMenuVisible = true;
	}

	public void StartNewGame() {
		Time.timeScale = 1;
		GameManager.StartGame ();
	}

	public void MainMenu() {
		Time.timeScale = 1;
		GameManager.ShowMainMenu ();
	}
}
