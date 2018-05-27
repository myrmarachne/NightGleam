using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDeathController : MonoBehaviour {

	private float maxSeconds = 1f;
	private Light spellLight;

	private float sign = 1f;

	// Use this for initialization
	void Awake () {
		spellLight = GetComponent<Light>();
		spellLight.range = 0f;

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		spellLight.range += (Time.deltaTime / (maxSeconds)) * 40f * sign;

		if (spellLight.range >= 2.5f)
			sign = -0.125f;

		if (spellLight.range <= 0f) {
			Destroy(this.gameObject);
		}
	}
}
