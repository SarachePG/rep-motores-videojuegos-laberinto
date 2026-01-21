using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] private Transform respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRAP ENTER -> " + other.name);

        if (!other.CompareTag("Player")) return;

        CharacterController cc = other.GetComponent<CharacterController>();
        if (cc != null) cc.enabled = false;

        other.transform.position = respawnPoint.position;

        if (cc != null) cc.enabled = true;

        Debug.Log("RESPAWN DONE!");
    }
}


