using System;
using UnityEngine;

public class Player {

    private float lifes;
	private float maxLifes;

	public Player(float maxLifes) {
		Reset(maxLifes);
    }


	public void Reset(float maxLifes) {
		this.lifes = maxLifes;
		this.maxLifes = maxLifes;
    }

	public void setLifes(float lifes){
		this.lifes = lifes;
		if (this.lifes < 0)
			this.lifes = 0;
		else if (this.lifes > this.maxLifes)
			this.lifes = this.maxLifes;
	}

	public float getLifes(){
		return this.lifes;
	}
}
