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
    // 1) Input por teclas directas (evita ejes mal configurados)
    float forward = 0f;
    if (Input.GetKey(KeyCode.W)) forward += 1f;
    if (Input.GetKey(KeyCode.S)) forward -= 1f;

    float strafe = 0f;
    if (Input.GetKey(KeyCode.D)) strafe += 1f;
    if (Input.GetKey(KeyCode.A)) strafe -= 1f;

    // 2) Giro con flechas (izquierda/derecha)
    float rotateInput = 0f;
    if (Input.GetKey(KeyCode.LeftArrow)) rotateInput -= 1f;
    if (Input.GetKey(KeyCode.RightArrow)) rotateInput += 1f;

    transform.Rotate(0f, rotateInput * rotationSpeed * Time.deltaTime, 0f);

    // 3) Movimiento relativo a la orientación del jugador
    Vector3 move = (transform.right * strafe + transform.forward * forward);

    // 4) Normalizar para que diagonal no vaya más rápido
    if (move.sqrMagnitude > 1f) move.Normalize();

    controller.Move(move * moveSpeed * Time.deltaTime);
}


}

