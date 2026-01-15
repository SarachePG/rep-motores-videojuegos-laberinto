using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 180f;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // 1) Leer input
        float horizontal = Input.GetAxis("Horizontal"); // A/D o flechas
        float vertical = Input.GetAxis("Vertical");     // W/S o flechas

        // 2) Rotar en Y (giro izquierda/derecha)
        float rotation = horizontal * rotationSpeed * Time.deltaTime;
        transform.Rotate(0f, rotation, 0f);

        // 3) Mover hacia adelante/atrás (según hacia dónde mire el Player)
        Vector3 move = transform.forward * (vertical * moveSpeed);

        // 4) Aplicar movimiento con CharacterController (y deltaTime)
        controller.Move(move * Time.deltaTime);
    }
}

