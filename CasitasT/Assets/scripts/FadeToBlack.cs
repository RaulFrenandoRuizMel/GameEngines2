using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    [SerializeField] private RawImage _rawImage;
    [SerializeField] private float DuracionFade = 2f;
    [SerializeField] private float SegundosDelay = 0f;

    private bool activado = false;
    private float timer = 0f;

    private void Update()
    {
        // Solo procesamos la lógica si el trigger fue activado
        if (!activado) return;

        timer += Time.deltaTime;

        // 1. Esperamos a que pase el tiempo de delay
        if (timer >= SegundosDelay)
        {
            // Si la imagen estaba apagada (por el script anterior), la encendemos aquí
            if (!_rawImage.enabled)
            {
                _rawImage.enabled = true;
            }

            // 2. Calculamos el tiempo real que lleva ocurriendo el fade
            float tiempoFade = timer - SegundosDelay;
            
            Color color = _rawImage.color;
            
            // Va de 0 (transparente) a 1 (negro total)
            color.a = Mathf.Clamp01(tiempoFade / DuracionFade);
            _rawImage.color = color;

            // 3. Cuando el fade termina, detenemos el proceso
            if (tiempoFade >= DuracionFade)
            {
                activado = false;
                timer = 0f; // Reiniciamos el timer para futuras activaciones
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Evita reiniciar el fade si ya está en progreso
        if (other.CompareTag("Player") && !activado)
        {
            // Forzamos que inicie completamente transparente (Alfa = 0)
            Color color = _rawImage.color;
            color.a = 0f;
            _rawImage.color = color;
            
            // Encendemos el interruptor y reseteamos el reloj
            activado = true;
            timer = 0f;
        }
    }
}