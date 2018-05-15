using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenuController : MonoBehaviour {
	public Button StartGameButton, QuitButton;
	protected virtual void Start() {
		StartGameButton.onClick.AddListener (startGame);
		QuitButton.onClick.AddListener (quit);
	}

	private void startGame() {
		SceneManager.LoadScene ("2DPlatformerMain");
	}

	private void quit() {
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit ();
#endif
	}
}
