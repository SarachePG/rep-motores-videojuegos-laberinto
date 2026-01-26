using UnityEngine;

public class Interruptor : MonoBehaviour
{
    [Header("Puerta asociada")]
    [SerializeField] private GameObject puerta;   // arrastra aquí Puerta_1
    [SerializeField] private bool abierto = false;

    public void Activar()
    {
        if (abierto) return;
        abierto = true;

        if (puerta != null)
        {
            // “Abrir” = desaparecer (simple y válido)
            puerta.SetActive(false);
        }

        Debug.Log("Interruptor activado: " + gameObject.name);
    }
}
