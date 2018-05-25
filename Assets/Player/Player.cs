using System;
using UnityEngine;

public class Player {

	private float brightness;
    private int lifes;
	private int maxLifes;

	public Player(int maxLifes) {
		Reset(maxLifes);
    }

	public void Reset(int maxLifes) {
		this.lifes = maxLifes;
		this.maxLifes = maxLifes;
		this.brightness = 1.0f;
    }

	public void SetLifes(int lifes){
		this.lifes = lifes;
		if (this.lifes < 0)
			this.lifes = 0;
		else if (this.lifes > this.maxLifes)
			this.lifes = this.maxLifes;
	}

	public int GetLifes(){
		return this.lifes;
	}

	public float GetBrightness() {
		return this.brightness;
	}

	public void SetBrightness(float brightness) {
		if (brightness > 0) {
			this.brightness = Math.Min(1, brightness);
		} else {
			this.brightness = 1.0f;
			this.SetLifes (this.GetLifes () - 1);
		}
	}
}
