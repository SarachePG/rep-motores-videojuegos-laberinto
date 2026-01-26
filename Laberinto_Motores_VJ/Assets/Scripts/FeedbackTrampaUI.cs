using System.Collections;
using TMPro;
using UnityEngine;

public class FeedbackTrampaUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI textoTrampa;

    private Coroutine rutina;

    private void Awake()
    {
        // ✅ Si no está asignado, lo busca automáticamente en hijos (incluso inactivos)
        if (textoTrampa == null)
            textoTrampa = GetComponentInChildren<TextMeshProUGUI>(true);

        // ✅ Si sigue sin existir, avisamos pero NO rompemos el juego
        if (textoTrampa == null)
        {
            Debug.LogWarning("[FeedbackTrampaUI] No está asignado Texto Trampa (Texto_Trampa).");
            return;
        }

        OcultarInmediato();
    }

    public void MostrarMensaje(string mensaje, float segundos)
    {
        if (textoTrampa == null) return;

        if (rutina != null) StopCoroutine(rutina);
        rutina = StartCoroutine(RutinaMostrar(mensaje, segundos));
    }

    private IEnumerator RutinaMostrar(string mensaje, float segundos)
    {
        textoTrampa.text = mensaje;
        textoTrampa.gameObject.SetActive(true);

        yield return new WaitForSeconds(segundos);

        OcultarInmediato();
        rutina = null;
    }

    private void OcultarInmediato()
    {
        if (textoTrampa == null) return;

        textoTrampa.text = "";
        textoTrampa.gameObject.SetActive(false);
    }
}
