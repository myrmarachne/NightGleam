using System;
using UnityEngine;

public class Player {

    private int lifes;
	private int maxLifes;

	public Player(int maxLifes) {
		Reset(maxLifes);
    }


	public void Reset(int maxLifes) {
		this.lifes = maxLifes;
		this.maxLifes = maxLifes;
    }

	public void setLifes(int lifes){
		this.lifes = lifes;
		if (this.lifes < 0)
			this.lifes = 0;
		else if (this.lifes > this.maxLifes)
			this.lifes = this.maxLifes;
	}

	public int getLifes(){
		return this.lifes;
	}
}
