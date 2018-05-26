using UnityEngine;

public class Item : MonoBehaviour {

    private static float loseProbability = 0.4f;

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.name == "Player") {
            Destroy(this.gameObject);
        }
    }

	public static int ElixirImpact() {
        /* The probability of losing one life is equal to loseProbability */
		if (Random.Range (0f, 1.0f) < loseProbability) {
			return -1;
		} else {
			return 1;
		}
    }

	public static float LightElixirImpact() {
		return 0.5f;
	}
}
