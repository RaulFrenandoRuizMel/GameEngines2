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
        if (_rawImage.enabled == false)
        {
            _rawImage.enabled = true;
            timer += Time.deltaTime;

            // Solo empezamos el fade si el timer ya superó los SegundosDelay
            if (timer > SegundosDelay)
            {
                // Tiempo real que lleva ocurriendo el fade
                float tiempoFade = timer - SegundosDelay;
                
                Color color = _rawImage.color;
                // Va de 0 (transparente) a 1 (negro total)
                color.a = Mathf.Lerp(0f, 1f, tiempoFade / DuracionFade);
                _rawImage.color = color;

                // Apagar el script cuando el fade termine para ahorrar rendimiento
                if (tiempoFade >= DuracionFade)
                {
                    activado = false;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !activado)
        {
            // Asegurar que inicie transparente y encender el contador
            Color color = _rawImage.color;
            color.a = 0f;
            _rawImage.color = color;
            
            activado = true;
            timer = 0f;
        }
    }
}