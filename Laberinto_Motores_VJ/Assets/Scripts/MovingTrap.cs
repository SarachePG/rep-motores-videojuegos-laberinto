using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingTrap : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    public Transform respawnPoint;

    private Vector3 target;

    void Start()
    {
        if (pointA != null) target = pointA.position;
    }

    void Update()
    {
        if (pointA == null || pointB == null) return;

        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Si llego a A, voy a B. Si llego a B, vuelvo a A.
        if (Vector3.Distance(transform.position, pointA.position) < 0.05f)
            target = pointB.position;

        else if (Vector3.Distance(transform.position, pointB.position) < 0.05f)
            target = pointA.position;
    }

    private void OnTriggerEnter(Collider other)
{
    Debug.Log("TRAMPA 2 ENTER -> " + other.name + " | Tag: " + other.tag);

    if (respawnPoint == null)
    {
        Debug.LogWarning("RespawnPoint NO asignado en Trampa_2_Movil");
        return;
    }

    // Respawn sin filtrar por tag (solo para comprobar)
    CharacterController cc = other.GetComponent<CharacterController>();
    if (cc != null) cc.enabled = false;

    other.transform.position = respawnPoint.position;

    if (cc != null) cc.enabled = true;

    Debug.Log("TRAMPA 2 RESPAWN DONE");
}

}

