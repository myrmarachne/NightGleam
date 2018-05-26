using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager {
	private static Game game = Game.GetInstance();
	private static int numberOfLevels = 2;

	public static void ShowMainMenu() {
		game.State = GameState.Stopped;
		SceneManager.LoadScene ("MainMenu");
	}

	public static void StartGame() {
		
		game.maxLifes = 3;
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
		if (n > numberOfLevels) {
			game.State = GameState.Won;
		} else {
			game.Level = n;
			SceneManager.LoadScene ("Level" + n.ToString());
		}
	}
}
