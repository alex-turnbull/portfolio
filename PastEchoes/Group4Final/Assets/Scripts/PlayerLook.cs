using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{

    public Transform playerBody;
    public Camera main;

    [Range(0.5f, 10f)]
    public float mouseSensitivity;

    float xAxisClamp = 0.0f;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //main.pixelRect = new Rect(0, 0, 800, 600);
    }

    void Update()
    {
        RotateCamera();
        Debug.DrawRay(gameObject.transform.position, gameObject.transform.forward * 5.0f, Color.green);
    }

    void RotateCamera()
    {
        if (GameHandler.Instance.gameState == GameHandler.gameStates.navigating)
        {
            Cursor.lockState = CursorLockMode.Locked;

            float MouseX = Input.GetAxis("Mouse X");
            float MouseY = Input.GetAxis("Mouse Y");

            float rotAmountX = MouseX * mouseSensitivity;
            float rotAmountY = MouseY * mouseSensitivity;

            xAxisClamp -= rotAmountY;

            Vector3 targetRotCam = transform.rotation.eulerAngles;
            Vector3 targetRotBody = playerBody.rotation.eulerAngles;

            targetRotCam.x -= rotAmountY;
            targetRotCam.z = 0;
            targetRotBody.y += rotAmountX;

            if (xAxisClamp > 90)
            {
                xAxisClamp = 90;
                targetRotCam.x = 90;
            }
            else if (xAxisClamp < -90)
            {
                xAxisClamp = -90;
                targetRotCam.x = 270;
            }


            transform.rotation = Quaternion.Euler(targetRotCam);
            playerBody.rotation = Quaternion.Euler(targetRotBody);
        }       
    }
}