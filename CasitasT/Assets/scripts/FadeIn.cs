using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class FadeIn : MonoBehaviour
{
    [Tooltip("Duración de la transición en segundos")]
    [SerializeField] private float duration = 1.5f;

    [Tooltip("¿Comenzar el fade in automáticamente al iniciar la escena?")]
    [SerializeField] private bool playOnStart = true;

    [Tooltip("¿Desactivar el objeto cuando termine el fade para que no bloquee clics (raycasts)?")]
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
        StopAllCoroutines();
        StartCoroutine(FadeInRoutine());
    }

    private IEnumerator FadeInRoutine()
    {
        // Asegura que empiece totalmente negro y visible
        gameObject.SetActive(true);
        Color color = _rawImage.color;
        color.a = 1f;
        _rawImage.color = color;

        float timer = 0f;

        // Va reduciendo el alfa de 1 a 0 de forma lineal
        while (timer < duration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, timer / duration);
            _rawImage.color = color;
            yield return null;
        }

        // Asegura el valor final exacto en 0
        color.a = 0f;
        _rawImage.color = color;

        if (deactivateOnComplete)
        {
            gameObject.SetActive(false);
        }
    }
}