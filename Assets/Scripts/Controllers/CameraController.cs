using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public float cameraSpeed = 0.2f;

    float horizontalMovement;
    float verticalMovement;
    float depthMovement;

    // drag 
    private Vector3 dragOrigin;
    
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray, out hit, 10);

        if (Input.GetMouseButtonDown(2))
        {
            dragOrigin = hit.point;
            return;
        }

        if (!Input.GetMouseButton(2)) return;
        
        Vector3 offset = dragOrigin - hit.point;
        offset.y = 0;

        Vector3 correctedPos = CorrectNewPosition(transform.position + offset);
        transform.Translate(correctedPos - transform.position, Space.World);
    }

    void FixedUpdate () {
        CheckInput();
        MoveCamera(CalculateNewCameraPosition());
    }

    /**
     * Checks for player input
     */
    void CheckInput()
    {
        horizontalMovement = Input.GetAxis("Horizontal") * cameraSpeed;
        verticalMovement = Input.GetAxis("Vertical") * cameraSpeed;
        depthMovement = Input.GetAxis("Mouse ScrollWheel") * 5f;
    }
    
    Vector3 CalculateNewCameraPosition()
    {
        Vector3 movement = new Vector3(horizontalMovement, 0f, verticalMovement);
        Vector3 newPosition = Camera.main.transform.position + movement;
        Camera.main.orthographicSize -= depthMovement;

        return CorrectNewPosition(newPosition);
    }

    /**
     * Calculates the movement of the camera and holds in within the game boundries
     */
    void MoveCamera(Vector3 newPosition)
    {
        transform.position = newPosition;
    }

    Vector3 CorrectNewPosition(Vector3 newPosition)
    {
        float camSize = Camera.main.orthographicSize;
        //  Check if camera is moving out of bounds. If so: adjust it to the edge
        if (newPosition.z < camSize)
        {
            newPosition.z = camSize;
        }
        if (newPosition.z > 15.0f - (camSize - 5f))
        {
            newPosition.z = 15.0f - (camSize - 5f);
        }

        //at the moment you can't move horizontally (intended)
        if (newPosition.x < 12f)
        {
            newPosition.x = 12f;
        }
        if (newPosition.x > 12f)
        {
            newPosition.x = 12f;
        }

        if (Camera.main.orthographicSize > 10)
            Camera.main.orthographicSize = 10;
        if (Camera.main.orthographicSize < 3)
            Camera.main.orthographicSize = 3;

        return newPosition;
    }
}
