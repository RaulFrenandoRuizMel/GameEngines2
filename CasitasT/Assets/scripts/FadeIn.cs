using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class FadeIn : MonoBehaviour
{
    [Tooltip("Duración de la transición en segundos")]
    [SerializeField] private float duration = 5.5f;

    [Tooltip("¿Comenzar el fade in automáticamente al iniciar la escena?")]
    [SerializeField] private bool playOnStart = true;

    [Tooltip("¿Desactivar el componente/bloqueo cuando termine el fade?")]
    [SerializeField] private bool deactivateOnComplete = true;

    private RawImage _rawImage;

    private void Awake()
    {
        _rawImage = GetComponent<RawImage>();
    }

    private void Start()
    {
        if (playOnStart)
        {
            StartFadeIn();
        }
    }

    [ContextMenu("Start Fade In")]
    public void StartFadeIn()
    {
        // Activamos el componente de imagen y los raycasts antes de iniciar
        _rawImage.enabled = true;
        _rawImage.raycastTarget = true;

        StopAllCoroutines();
        StartCoroutine(FadeInRoutine());
    }

    private IEnumerator FadeInRoutine()
    {
        Color color = _rawImage.color;
        color.a = 1f;
        _rawImage.color = color;

        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            // Usamos Mathf.Clamp01 por seguridad en la división
            _rawImage.color = new Color(color.r, color.g, color.b, Mathf.Lerp(1f, 0f, timer / duration));
            yield return null;
        }

        color.a = 0f;
        _rawImage.color = color;

        if (deactivateOnComplete)
        {
            // Apagamos la imagen y los clics en vez de apagar TODO el GameObject
            _rawImage.enabled = false;
            _rawImage.raycastTarget = false;
        }
    }
}