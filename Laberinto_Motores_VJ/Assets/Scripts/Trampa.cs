using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Collider))]
public class Trampa : MonoBehaviour
{
    [Header("Jugador")]
    [SerializeField] private string etiquetaJugador = "Player";
    [SerializeField] private Transform puntoInicio;

    [Header("UI (Texto de trampa)")]
    [SerializeField] private TextMeshProUGUI textoTrampaUI; // Arrastra Canvas > Texto_Trampa
    [SerializeField] private string mensajeTrampa = "Has caído en una trampa.";
    [SerializeField] private float segundosMensaje = 1.2f;

    [Header("Tiempo")]
    [SerializeField] private float retrasoTeletransporte = 0.3f;

    [Header("Color")]
    [SerializeField] private Renderer renderTrampa;
    [SerializeField] private Color colorActivado = Color.red;

    private Color colorOriginal;
    private bool activada;

    private void Awake()
    {
        // Trigger siempre
        GetComponent<Collider>().isTrigger = true;

        // Renderer (si no lo arrastras, lo busca)
        if (renderTrampa == null)
            renderTrampa = GetComponentInChildren<Renderer>();

        if (renderTrampa != null)
            colorOriginal = renderTrampa.material.color;

        // Texto UI (si no lo arrastras, lo busca por nombre exacto)
        if (textoTrampaUI == null)
        {
            GameObject obj = GameObject.Find("Texto_Trampa");
            if (obj != null)
                textoTrampaUI = obj.GetComponent<TextMeshProUGUI>();
        }

        // Ocultar al inicio
        if (textoTrampaUI != null)
            textoTrampaUI.gameObject.SetActive(false);
        else
            Debug.LogWarning("[Trampa] No se encontró Texto_Trampa. Arrástralo en el Inspector.");
    }

    private void OnTriggerEnter(Collider otro)
    {
        if (activada) return;
        if (!otro.CompareTag(etiquetaJugador)) return;

        activada = true;

        // Mostrar mensaje
        if (textoTrampaUI != null)
        {
            textoTrampaUI.text = mensajeTrampa;
            textoTrampaUI.gameObject.SetActive(true);
        }

        // Cambiar color
        if (renderTrampa != null)
            renderTrampa.material.color = colorActivado;

        StartCoroutine(RutinaTrampa(otro.gameObject));
    }

    private IEnumerator RutinaTrampa(GameObject jugador)
    {
        // Mantener un poco el mensaje visible antes del respawn
        yield return new WaitForSeconds(retrasoTeletransporte);

        // Teletransportar al inicio
        if (puntoInicio != null)
        {
            CharacterController cc = jugador.GetComponent<CharacterController>();
            if (cc != null) cc.enabled = false;

            Rigidbody rb = jugador.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            jugador.transform.SetPositionAndRotation(puntoInicio.position, puntoInicio.rotation);

            if (cc != null) cc.enabled = true;
        }
        else
        {
            Debug.LogWarning("[Trampa] PuntoInicio NO asignado.");
        }

        // Espera extra para que el jugador lea el mensaje
        yield return new WaitForSeconds(Mathf.Max(0f, segundosMensaje - retrasoTeletransporte));

        // Ocultar mensaje
        if (textoTrampaUI != null)
            textoTrampaUI.gameObject.SetActive(false);

        // Restaurar color
        if (renderTrampa != null)
            renderTrampa.material.color = colorOriginal;

        activada = false;
    }
}
