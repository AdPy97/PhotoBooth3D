using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelController : MonoBehaviour
{
    [SerializeField]
    private float rotationSensitivity;
    private GameObject currentChosenModel;

    private void Update()
    {
        InputManager();
    }

    public void InputManager()
    {
        if (Input.GetMouseButton(2))
        {
            StartRotation();
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (Input.GetMouseButton(1))
        {
            StartMoving();
            Cursor.lockState = CursorLockMode.Locked;
        }
        else if (Input.mouseScrollDelta.y != 0)
            StartZooming();
        else
            Cursor.lockState = CursorLockMode.None; 
    }

    private void StartRotation()
    {
        currentChosenModel.transform.Rotate(-Input.GetAxis("Mouse Y") * rotationSensitivity,
            Input.GetAxis("Mouse X") * rotationSensitivity, 0);
    }

    private void StartZooming()
    {
        currentChosenModel.transform.position += new Vector3(0, 0, Input.mouseScrollDelta.y);
    }

    private void StartMoving()
    {
        currentChosenModel.transform.position += new Vector3(Input.GetAxis("Mouse X"),
            Input.GetAxis("Mouse Y"), 0);
    }

    public void ResetTransform()
    {
        currentChosenModel.transform.position = Vector3.zero;
        currentChosenModel.transform.rotation = Quaternion.identity;
    }

    public void SetCurrentModel(GameObject gameObject)
    {
        currentChosenModel = gameObject;
    }
}
