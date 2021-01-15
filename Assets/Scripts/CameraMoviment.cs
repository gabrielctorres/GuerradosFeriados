using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoviment : MonoBehaviour
{

    float speed = 0.3f;
    float movimentTime = 0.5f;
    Vector3 newPosition;
    public float minX, maxX;
    public float minY, maxY;
    Camera mainCamera;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GetComponent<Camera>();
        newPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CameraFollowMouse();
        ClampMovimentCamera();

        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if(mainCamera.orthographicSize < 9)
            {
                mainCamera.orthographicSize++;
            }
            
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {            
            if (mainCamera.orthographicSize > 3)
            {
                mainCamera.orthographicSize--;
            }
        }

    }

    void CameraFollowMouse()
    {

        if (Input.GetKey(KeyCode.W))
        {
            newPosition += (transform.up * speed);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            newPosition += (transform.up * -speed);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            newPosition += (transform.right * speed);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            newPosition += (transform.right * -speed);
        }
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * movimentTime);
    }

    void ClampMovimentCamera()
    {
        if (transform.position.x <= minX || transform.position.x >= maxX)
        {


            float xPos = Mathf.Clamp(transform.position.x, minX, maxX);


            transform.position = new Vector3(xPos, transform.position.y, transform.position.z);

        }
        if (transform.position.y <= -minY || transform.position.y >= maxY)
        {


            float YPos = Mathf.Clamp(transform.position.y, minY, maxY);


            transform.position = new Vector3(transform.position.x, YPos, transform.position.z);

        }
    }
}
