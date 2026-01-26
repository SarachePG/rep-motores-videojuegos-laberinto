using UnityEngine;

public class TrampaMovil : MonoBehaviour
{
    public enum TipoMovimiento { PingPong, Bucle }

    [Header("Movimiento")]
    [SerializeField] private TipoMovimiento tipoMovimiento = TipoMovimiento.PingPong;
    [SerializeField] private Vector3 desplazamiento = new Vector3(0, 0, 3);
    [SerializeField] private float velocidad = 2f;

    private Vector3 posicionInicial;
    private float tiempo;

    private void Start()
    {
        posicionInicial = transform.localPosition;
    }

    private void Update()
    {
        tiempo += Time.deltaTime * velocidad;

        float valor = tipoMovimiento == TipoMovimiento.PingPong
            ? Mathf.PingPong(tiempo, 1)
            : tiempo % 1;

        transform.localPosition = Vector3.Lerp(
            posicionInicial,
            posicionInicial + desplazamiento,
            valor
        );
    }
}
