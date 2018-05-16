using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenuController : MonoBehaviour {
	public Button StartGameButton, QuitButton;

	private GameController gameController = GameController.instance; //???


	protected void Awake() {
		
		QuitButton.onClick.AddListener (gameController.Quit);
		StartGameButton.onClick.AddListener (gameController.StartGame);

	}
		
		
}
