using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour {
	public Button ContinueButton, MainMenuButton;
	protected virtual void Start() {
		ContinueButton.onClick.AddListener (continueGame);
		MainMenuButton.onClick.AddListener (mainMenu);
	}

	private void mainMenu() {
		SceneManager.LoadScene ("MainMenu");
	}

	private void continueGame() {
		SceneManager.LoadScene ("2DPlatformerMain");
	}
}
