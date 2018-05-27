using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {
	private const float minLightScale = 20;
	private const float maxLightScale = 70;
	private const float timeToRegenerateLight = 30.0f;
	private const float lightRegenerationStep = 0.2f;

	private Game game = Game.GetInstance();
	private float lightRegenerationClock = 0.0f;
	private float lastBrightness;

	private void Start() {
		lastBrightness = game.Player.GetBrightness ();
	}

	private void FixedUpdate() {
		HandleLightRegeneration ();
		Transform transform = GetComponent<Transform> ();
		float brightness = game.Player.GetBrightness ();
		float newScale = minLightScale + brightness * (maxLightScale - minLightScale);
		transform.localScale = new Vector3 (newScale, newScale, 0);
	}

	private void HandleLightRegeneration() {
		float playerBrightness = game.Player.GetBrightness ();
		// reset clock if light is 100% or player just has lost his light
		if (lastBrightness == 1.0f || lastBrightness > playerBrightness) {
			lightRegenerationClock = 0.0f;
			lastBrightness = playerBrightness;
		}
		lightRegenerationClock += Time.deltaTime;
		if (lightRegenerationClock >= timeToRegenerateLight) {
			lightRegenerationClock -= timeToRegenerateLight;
			playerBrightness += lightRegenerationStep;
			game.Player.SetBrightness (playerBrightness);
			lastBrightness = playerBrightness;
		}
	}
}
