using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {
	public Button StartGameButton, QuitButton;

	protected void Start() {
		QuitButton.onClick.AddListener (GameManager.QuitGame);
		StartGameButton.onClick.AddListener (GameManager.StartGame);
	}
}
