using System;
using UnityEngine.SceneManagement;

public class GameManager {
	private static Game game = Game.GetInstance();

	public static void ShowMainMenu() {
		game.State = GameState.Stopped;
		SceneManager.LoadScene ("MainMenu");
	}

	public static void StartGame() {
		game.Reset ();
		GoToNthLevel (1);
	}

	public static void QuitGame() {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit ();
		#endif
	}

	public static void GoToNthLevel(int n) {
		game.Level = n;
		//TODO handle nth level
		SceneManager.LoadScene ("2DPlatformerMain");
	}
}
