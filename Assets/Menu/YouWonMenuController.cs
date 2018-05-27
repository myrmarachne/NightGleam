using UnityEngine;
using UnityEngine.UI;

public class YouWonMenuController : GameOverMenuController {
	protected virtual void Start() {
		targetState = GameState.Won;
		canvasGroup = GetComponent<CanvasGroup> ();

		// Attach buttons click event listeners
		Button startNewGameButton = GameObject.Find ("YouWonMenuStartNewGameButton").GetComponent<Button>();
		Button mainMenuButton = GameObject.Find ("YouWonMenuMainMenuButton").GetComponent<Button>();
		startNewGameButton.onClick.AddListener (StartNewGame);
		mainMenuButton.onClick.AddListener (MainMenu);
	}
}
