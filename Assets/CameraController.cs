using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    /* TODO: Na razie kamera podąża cały czas za postacią, 
     * należy zmienić to w taki sposób, aby kamera poruszała
     * się za postacią wyłącznie prawo-lewo cały czas, z kolei
     * góra-dół - wyłącznie przy zmianie "poziomu" */

    public GameObject mainCharacter;

    // Offset distane between mainCharacter and the camera
    private Vector3 offset;

	// Use this for initialization
	void Start () {

        /* Calculate the distance between transform.position of camera
         * and the transform.position of the mainCharacter */

        offset = transform.position - mainCharacter.transform.position;
	}
	
	// Runs after the Update() function
	void LateUpdate () {
        transform.position = mainCharacter.transform.position + offset;
	}
}
