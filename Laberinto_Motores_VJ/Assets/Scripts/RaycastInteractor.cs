using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastInteractor : MonoBehaviour
{
    public Camera playerCamera;
    public float interactDistance = 3f;
    public LayerMask interactLayer;

    void Update()
{
    if (playerCamera == null) return;

    Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

    if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
    {
        Debug.Log("Hit: " + hit.collider.name);
    }
}

}
