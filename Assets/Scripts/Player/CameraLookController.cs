using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class manage the first person view of the player from the mouse inputs.
/// <param name="m_camera">the camera</param>
/// <param name="m_sensitivity">the speed of the camera movement</param>"
/// </summary>
public class CameraLookController : MonoBehaviour
{
    [SerializeField]
    protected Camera m_camera;
    [SerializeField]
    protected float m_sensitivity = 100f;

    protected float m_xRotation = 0f;

    protected void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    protected void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * m_sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * m_sensitivity * Time.deltaTime;

        m_xRotation -= mouseY;
        m_xRotation = Mathf.Clamp(m_xRotation, -80f, 80f);

        m_camera.transform.localRotation = Quaternion.Euler(m_xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }
}
