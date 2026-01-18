using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaycastInteractor : MonoBehaviour
{
    public Camera playerCamera;
    public float interactDistance = 5f;
    public LayerMask interactLayer;

    public TextMeshProUGUI interactText;

    void Update()
    {
        if (playerCamera == null) return;

        bool mirandoInteractuable = false;

        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactLayer))
        {
            mirandoInteractuable = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                Switch sw = hit.collider.GetComponent<Switch>();
                if (sw != null) sw.Activar();
            }
        }

        if (interactText != null)
            interactText.gameObject.SetActive(mirandoInteractuable);
    }
}
