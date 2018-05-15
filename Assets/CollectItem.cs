using UnityEngine;

public class CollectItem : MonoBehaviour {

    private float loseProbability = 0.4f;
    private Game game = Game.GetInstance();

    private void OnTriggerEnter2D(Collider2D collider) {
        if (collider.name == "Player") {
            /* Change the amount of life of the Player after collecting
             * the Elixir */

            game.Player.Lifes += ElixirImpact();
            Destroy(this.gameObject);
        }
    }

    private int ElixirImpact() {
        /* The probability of losing one life is equal to loseProbability */
        if (Random.Range(0f, 1.0f) < loseProbability) {
            return -1;
        } else {
            return 1;
        }
    }
}
