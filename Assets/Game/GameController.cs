using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
	
    private Game game = Game.GetInstance();
	public static GameController instance = null;

	private LevelController levelController = LevelController.instance; // ??? czy to ma sens w ogole

    private int level = 1;

	protected void Awake() {

		/* Singleton pattern */

		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		/* Don't destroy game controller on scene loading */
		DontDestroyOnLoad (gameObject);

	}

	protected void Start() {
		game.Player.Reset ();
	}

    void Update () {
        if (game.State == GameState.Playing && game.Player.Lifes == 0) {
			levelController.LoadGameOverMenu ();
        }
	}

	public void Quit() {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit ();
		#endif
	}

	public void StartGame() {
		SceneManager.LoadScene ("2DPlatformerMain");
	}
}
