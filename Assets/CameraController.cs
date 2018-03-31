using UnityEngine;

public class CameraController : MonoBehaviour {

    /* TODO: Na razie kamera podąża cały czas za postacią, 
     * należy zmienić to w taki sposób, aby kamera poruszała
     * się za postacią wyłącznie prawo-lewo cały czas, z kolei
     * góra-dół - wyłącznie przy zmianie "poziomu" */

    private PlayerController playerController;

    // Offset distane between mainCharacter and the camera
    private Vector3 offset;

	// Use this for initialization
	void Start () {

        playerController = FindObjectOfType<PlayerController>();

        /* Calculate the distance between transform.position of camera
         * and the transform.position of the playerController game object */

        offset = transform.position - playerController.gameObject.transform.position;
    }

    // Runs after the Update() function
    void LateUpdate () {

        Vector3 camera = Vector3.zero;

        if (playerController.changeCamera) {
            
            /* Change the camera vertically */
            camera.x = playerController.gameObject.transform.position.x + offset.x;
            camera.z = transform.position.z;
            camera.y = playerController.gameObject.transform.position.y + offset.y;

            playerController.changeCamera = false;
            
        } else {

            /* Change the camera horizontally */

            camera.x = playerController.gameObject.transform.position.x + offset.x;
            camera.z = transform.position.z;
            camera.y = transform.position.y;
        }

        transform.position = camera;
    }

    
}
