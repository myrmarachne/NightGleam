using UnityEngine;

public class CollectItem : MonoBehaviour {

    public float loseProbability = 0.4f;

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.GetComponent<Collider2D>().name == "Player") {
            /* Change the amount of life of the Player after collecting
             * the Elixir */
            GameController.gameController.LifeChange(ElixirImpact());
            this.gameObject.SetActive(false);
            Destroy(this);
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
