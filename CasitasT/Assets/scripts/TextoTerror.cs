using System.Collections;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TMP_Text))]
public class TextoTerror : MonoBehaviour
{
    [Header("Texto")]
    [TextArea(3, 8)]
    [SerializeField] private string dialogueText = "No debiste... entrar aquí.";
    [SerializeField] private bool playOnStart = false; // Desactivado por defecto si lo usas con trigger

    [Header("Desaparición al Finalizar")]
    [Tooltip("¿Ocultar el texto una vez termine el diálogo?")]
    [SerializeField] private bool hideAfterFinished = true;
    [Tooltip("Segundos que permanece visible tras terminar de escribirse")]
    [SerializeField] private float hideDelay = 2f;
    [Tooltip("¿Desactivar todo el GameObject del texto o solo borrar el texto?")]
    [SerializeField] private bool deactivateGameObject = false;
    [Tooltip("Objeto externo adicional a desactivar al terminar (panel, fondo, etc.)")]
    [SerializeField] private GameObject targetObjectToDeactivate;

    [Header("Tiempos (Efecto Errático)")]
    [Tooltip("Tiempo mínimo entre letras")]
    [SerializeField] private float minTypingSpeed = 0.04f;
    [Tooltip("Tiempo máximo entre letras (crea tartamudeo tétrico)")]
    [SerializeField] private float maxTypingSpeed = 0.12f;
    [Tooltip("Pausa adicional cuando encuentra signos de puntuación (. , ! ? ...)")]
    [SerializeField] private float punctuationPause = 0.6f;

    [Header("Audio (Opcional)")]
    [Tooltip("AudioSource para los clicks o chasquidos de cada letra")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip typeSound;
    [Range(0.1f, 2f)]
    [SerializeField] private float minPitch = 0.7f;
    [Range(0.1f, 2f)]
    [SerializeField] private float maxPitch = 0.9f;

    private TMP_Text _textMesh;
    private Coroutine _typeCoroutine;

    private void Awake()
    {
        _textMesh = GetComponent<TMP_Text>();
    }

    private void Start()
    {
        if (playOnStart)
        {
            StartDialogue(dialogueText);
        }
        else
        {
            // Limpia el texto al inicio para que no se vea nada antes del trigger
            _textMesh.text = "";
        }
    }

    [ContextMenu("Play Dialogue")]
    public void PlayCurrentDialogue()
    {
        StartDialogue(dialogueText);
    }

    public void StartDialogue(string newText = null)
    {
        string textToPlay = string.IsNullOrEmpty(newText) ? dialogueText : newText;

        // Si el objeto estuviera inactivo, nos aseguramos de encenderlo
        if (!gameObject.activeSelf) gameObject.SetActive(true);

        if (_typeCoroutine != null) StopCoroutine(_typeCoroutine);
        _typeCoroutine = StartCoroutine(TypeRoutine(textToPlay));
    }

    private IEnumerator TypeRoutine(string text)
    {
        _textMesh.text = "";

        for (int i = 0; i < text.Length; i++)
        {
            char c = text[i];
            _textMesh.text += c;

            if (audioSource != null && typeSound != null && !char.IsWhiteSpace(c))
            {
                audioSource.pitch = Random.Range(minPitch, maxPitch);
                audioSource.PlayOneShot(typeSound);
            }

            if (c == '.' || c == ',' || c == '?' || c == '!' || c == '…')
            {
                yield return new WaitForSeconds(punctuationPause);
            }
            else
            {
                float randomDelay = Random.Range(minTypingSpeed, maxTypingSpeed);
                yield return new WaitForSeconds(randomDelay);
            }
        }

        // Lógica de desaparición
        if (hideAfterFinished)
        {
            yield return new WaitForSeconds(hideDelay);

            // Desactiva el objeto externo si se configuró uno
            if (targetObjectToDeactivate != null)
            {
                targetObjectToDeactivate.SetActive(false);
            }

            // Desactiva o limpia el propio texto
            if (deactivateGameObject)
            {
                gameObject.SetActive(false);
            }
            else
            {
                _textMesh.text = "";
            }
        }
    }
}