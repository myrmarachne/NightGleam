using UnityEngine;
using UnityEngine.UI;

public class LevelWelcomeController : MonoBehaviour {
	private Game game = Game.GetInstance();
	private float visibleTimeLeft = 1.0f;

	protected virtual void Start() {
		Time.timeScale = 0;

		GameObject title = GameObject.Find ("LevelWelcomeTitle");
		if (title) {
			title.GetComponent<Text> ().text = "Level " + game.Level;
		}
	}

	protected virtual void Update(){
		visibleTimeLeft -= Time.unscaledDeltaTime;
		if (visibleTimeLeft <= 0) {
			Time.timeScale = 1;
			Destroy (this.gameObject);
		}
	}
}
