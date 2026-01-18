using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject puerta;   // arrastra aquí Puerta_1
    public bool abierto = false;

    public void Activar()
    {
        if (abierto) return;

        abierto = true;

        if (puerta != null)
        {
            puerta.SetActive(false); // “abrir” = desaparecer (simple y válido)
        }

        Debug.Log("Interruptor activado: " + gameObject.name);
    }
}
