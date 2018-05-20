using UnityEngine;

public class Item : MonoBehaviour {

    private static float loseProbability = 0.5f;
	private static float winHalfProbability = 0.25f;

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.name == "Player") {
            Destroy(this.gameObject);
        }
    }

	public static float ElixirImpact() {
        /* The probability of losing one life is equal to loseProbability */
        if (Random.Range(0f, 1.0f) < loseProbability) {
            return -0.5f;
		} else if  (Random.Range(0f, 1.0f) < winHalfProbability) {
			return 0.5f;
		} else {
			return 1.0f;
		}
    }
}
