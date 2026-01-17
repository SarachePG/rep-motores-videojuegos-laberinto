using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class CameraLook : MonoBehaviour
{
    public float lookSpeed = 80f;
    public float minPitch = -60f;
    public float maxPitch = 60f;

    float pitch = 0f;

    void Update()
    {
        float lookInput = 0f;

        if (Input.GetKey(KeyCode.UpArrow)) lookInput = 1f;
        if (Input.GetKey(KeyCode.DownArrow)) lookInput = -1f;

        pitch -= lookInput * lookSpeed * Time.deltaTime;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }
}

