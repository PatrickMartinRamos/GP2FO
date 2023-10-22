using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraDrag : MonoBehaviour
{
    private Vector3 dragOrigin;
    public Cinemachine.CinemachineFreeLook freeLookCamera;

    public float dragSpeed = 1.0f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Vector3 difference = dragOrigin - Input.mousePosition;

        // Adjust the axis values for your camera
        freeLookCamera.m_XAxis.Value += difference.x * dragSpeed;

        dragOrigin = Input.mousePosition;
    }
}
