using UnityEngine;

public class MovimientoJugador : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform camaraJugador;

    [Header("Movimiento")]
    [SerializeField] private float velocidadMovimiento = 5f;

    [Header("Visión (flechas)")]
    [SerializeField] private float velocidadGiroHorizontal = 180f; // ◀️▶️
    [SerializeField] private float velocidadGiroVertical = 120f;   // ▲▼
    [SerializeField] private float limiteArriba = 60f;
    [SerializeField] private float limiteAbajo = 60f;

    private CharacterController controlador;
    private float anguloVertical = 0f;

    private void Start()
    {
        controlador = GetComponent<CharacterController>();

        if (camaraJugador != null)
        {
            
            anguloVertical = NormalizarAngulo(camaraJugador.localEulerAngles.x);
        }
    }

    private void Update()
    {
        if (controlador == null) return;

       
        float adelante = 0f;
        if (Input.GetKey(KeyCode.W)) adelante += 1f;
        if (Input.GetKey(KeyCode.S)) adelante -= 1f;

        float lateral = 0f;
        if (Input.GetKey(KeyCode.D)) lateral += 1f;
        if (Input.GetKey(KeyCode.A)) lateral -= 1f;

        Vector3 movimiento = transform.right * lateral + transform.forward * adelante;
        if (movimiento.sqrMagnitude > 1f) movimiento.Normalize();

        controlador.Move(movimiento * velocidadMovimiento * Time.deltaTime);

       
        float giroHorizontal = 0f;
        if (Input.GetKey(KeyCode.LeftArrow)) giroHorizontal -= 1f;
        if (Input.GetKey(KeyCode.RightArrow)) giroHorizontal += 1f;

        transform.Rotate(0f, giroHorizontal * velocidadGiroHorizontal * Time.deltaTime, 0f);

       
        if (camaraJugador != null)
        {
            float giroVertical = 0f;
            if (Input.GetKey(KeyCode.UpArrow)) giroVertical -= 1f;  
            if (Input.GetKey(KeyCode.DownArrow)) giroVertical += 1f; 

            anguloVertical += giroVertical * velocidadGiroVertical * Time.deltaTime;
            anguloVertical = Mathf.Clamp(anguloVertical, -limiteArriba, limiteAbajo);

            camaraJugador.localRotation = Quaternion.Euler(anguloVertical, 0f, 0f);
        }
    }

    private float NormalizarAngulo(float angulo)
    {
        if (angulo > 180f) angulo -= 360f;
        return angulo;
    }
}
