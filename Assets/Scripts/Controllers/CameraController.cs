using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public float cameraSpeed = 0.2f;

    float horizontalMovement;
    float verticalMovement;

    // Use this for initialization
    void Start () {
    }
    
    // Update is called once per frame
    void FixedUpdate () {
        CheckInput();
        MoveCamera();
    }

    /**
     * Checks for player input
     */
    void CheckInput()
    {
        horizontalMovement    = Input.GetAxis("Horizontal") * cameraSpeed;
        verticalMovement      = Input.GetAxis("Vertical") * cameraSpeed;
    }

    /**
     * Calculates the movement of the camera and holds in within the game boundries
     */
    void MoveCamera()
    {
        Camera mainCamera       = Camera.main;
        Vector3 movement        = new Vector3(horizontalMovement, 0f, verticalMovement);
        Vector3 newPosition     = mainCamera.transform.position + movement;

        //  Check if camera is moving out of bounds. If so: adjust it to the edge
        if (newPosition.z < 3.5f)
        {
            newPosition.z = 3.5f;
        }
        if (newPosition.z > 15.0f)
        {
            newPosition.z = 15.0f;
        }
        if (newPosition.x < 10.0f)
        {
            newPosition.x = 10.0f;
        }
        if (newPosition.x > 15)
        {
            newPosition.x = 15;
        }

        transform.position = newPosition;
    }
}
