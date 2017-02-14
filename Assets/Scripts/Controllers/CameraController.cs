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
        Vector3 movement        = new Vector3(horizontalMovement, verticalMovement, 0f);
        Vector3 newPosition     = mainCamera.transform.position + movement;
        float verticalLength    = mainCamera.orthographicSize;
        float aspectRatio       = mainCamera.aspect;

        float topEdge   = newPosition.y + verticalLength;
        float botEdge   = newPosition.y - verticalLength;
        float leftEdge  = newPosition.x - verticalLength * aspectRatio;
        float rightEdge = newPosition.x + verticalLength * aspectRatio;


        //  Check if camera is moving out of bounds. If so: adjust it to the edge
        if (topEdge > 5)
        {
            newPosition.y = 5 - verticalLength;
        }
        if (botEdge < -5)
        {
            newPosition.y = -5 + verticalLength;
        }
        if (leftEdge < -10)
        {
            newPosition.x = -10 + verticalLength * aspectRatio;
        }
        if (rightEdge > 10)
        {
            newPosition.x = 10 - verticalLength * aspectRatio;
        }
        
        transform.position = newPosition;
    }
}
