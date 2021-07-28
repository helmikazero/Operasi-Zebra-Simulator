using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PolisiControl : MonoBehaviour
{
    public GameObject papanJalan;

    public Transform restPos;
    public Transform usePos;
    public bool isUsing;

    public float speed = 5f;

    public FPSView fpsview;

    public KeyCode useKey;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isUsing = Input.GetKey(useKey);

        switch (isUsing)
        {
            case true:
                papanJalan.transform.localPosition = Vector3.Slerp(papanJalan.transform.localPosition, usePos.localPosition, speed * Time.deltaTime);
                papanJalan.transform.localRotation = Quaternion.Slerp(papanJalan.transform.localRotation, usePos.localRotation, speed * Time.deltaTime);
                fpsview.TurnONMouse();
                break;
            case false:
                papanJalan.transform.localPosition = Vector3.Slerp(papanJalan.transform.localPosition, restPos.localPosition, speed * Time.deltaTime);
                papanJalan.transform.localRotation = Quaternion.Slerp(papanJalan.transform.localRotation, restPos.localRotation, speed * Time.deltaTime);
                fpsview.TurnOffMouse();
                break;
        }
    }
}
