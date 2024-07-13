using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform player;
    public float mouseSensitivity = 2;
    float cameraRotationY = 0f;
    float cameraRotationX = 0f;
    bool lockedCursor = true;


    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        //Collecting mouse input
        float inputX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float inputY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        cameraRotationY -= inputY;
        cameraRotationY = Mathf.Clamp(cameraRotationY, -90f, 90f);
        transform.localEulerAngles = Vector3.right * cameraRotationY;

        player.Rotate(Vector3.up * inputX);

    }
}
