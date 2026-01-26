using UnityEngine;
using TMPro;

public class InteraccionJugador : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Camera camara;
    [SerializeField] private TextMeshProUGUI textoInteraccion; // Canvas > InteractText

    [Header("Interacción")]
    [SerializeField] private float distanciaInteraccion = 5f;
    [SerializeField] private LayerMask capaInteractuable; // solo Interactable
    [SerializeField] private KeyCode teclaInteraccion = KeyCode.E;

    private void Awake()
    {
        if (camara == null) camara = GetComponent<Camera>();

        // ✅ Ocultar SIEMPRE al empezar
        if (textoInteraccion != null)
            textoInteraccion.gameObject.SetActive(false);
        else
            Debug.LogWarning("[InteraccionJugador] No está asignado el texto InteractText.");
    }

    private void Update()
    {
        if (camara == null || textoInteraccion == null) return;

        bool mostrar = false;
        Interruptor interruptorDetectado = null;

        Ray rayo = new Ray(camara.transform.position, camara.transform.forward);

        // ✅ Ignoramos triggers para no detectar trampas/zonas
        if (Physics.Raycast(rayo, out RaycastHit hit, distanciaInteraccion, capaInteractuable, QueryTriggerInteraction.Ignore))
        {
            // ✅ CLAVE: busca el interruptor en el PADRE también
            interruptorDetectado = hit.collider.GetComponentInParent<Interruptor>();

            if (interruptorDetectado != null)
                mostrar = true;
        }

        // Mostrar / ocultar sin parpadeos
        if (textoInteraccion.gameObject.activeSelf != mostrar)
            textoInteraccion.gameObject.SetActive(mostrar);

        // Interactuar
        if (interruptorDetectado != null && Input.GetKeyDown(teclaInteraccion))
            interruptorDetectado.Activar();
    }
}
