using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour {

	private static Game game = Game.GetInstance();

	public void GoToNextLevel(){
		GameManager.GoToNthLevel (game.Level+1);
	}

}
