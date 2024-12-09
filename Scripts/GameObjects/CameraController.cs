using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    private Camera playerCamera;
    [SerializeField] public CursorLockMode CursorState;
    [SerializeField] public float MouseSensitivity;
    [SerializeField] private Vector2 cameraClamp;

    Vector2 lookDelta;

    float CameraX;
    float CameraY;

    void Awake()
    {
        Cursor.lockState = CursorState;
        Cursor.visible = false;
        playerCamera = GetComponentInChildren<Camera>();
    }

    public void LookAction (InputAction.CallbackContext context)
    {
        lookDelta = context.ReadValue<Vector2>();

        CameraY += process(lookDelta.x);
        CameraX -= process(lookDelta.y);
        CameraX = Mathf.Clamp(CameraX, cameraClamp.x, cameraClamp.y);

        playerCamera.transform.localRotation = Quaternion.Euler(CameraX, 0, 0);
        this.transform.rotation = Quaternion.Euler(0, CameraY, 0);
    }

    private float process(float value) => value * MouseSensitivity * Time.deltaTime;
}
