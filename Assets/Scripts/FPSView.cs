using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSView : MonoBehaviour
{

    public playerProfile pp;
    public float mouseSensitivity = 300f;

    public Transform playerBody;
    public Transform playerCamObject;
    float xRotation = 0f;
    public float sensMultiplier = 1f;
    public bool lookActive = true;

    public bool cursorActive;

    // Start is called before the first frame update
    void Start()
    {
        pp = GameObject.Find("PLAYER_PROFILE").GetComponent<playerProfile>();
        sensMultiplier = pp.mouseSensitivityMultiplier/100f;
    }

    // Update is called once per frame
    void Update()
    {
        if (lookActive)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * sensMultiplier * Time.fixedDeltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * sensMultiplier * Time.fixedDeltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            playerCamObject.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }

        if (!cursorActive)
        {
            Cursor.lockState = CursorLockMode.Locked;
            lookActive = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            lookActive = false;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (!cursorActive)
            {
                cursorActive = true;
            }
            else
            {
                cursorActive = false;
            }
        }
    }

    public void TurnOffMouse()
    {
        cursorActive = false;
    }

    public void TurnONMouse()
    {
        cursorActive = true;
    }
}
