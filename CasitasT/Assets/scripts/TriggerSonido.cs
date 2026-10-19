using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggerSonido : MonoBehaviour
{
    [Header("Configuración del Disparador")]
    [Tooltip("Tag del objeto que activa el trigger (normalmente 'Player')")]
    [SerializeField] private string targetTag = "Player";
    [Tooltip("¿Se destruye el trigger tras activarse para sonar solo una vez?")]
    [SerializeField] private bool triggerOnlyOnce = true;

    [Header("Objetivo Remoto")]
    [Tooltip("Arrastra aquí el AudioSource ubicado donde debe originarse el sonido")]
    [SerializeField] private AudioSource remoteAudioSource;

    [Header("Parámetros de Audio")]
    [Tooltip("Si se deja vacío, usará el clip asignado previamente en el AudioSource")]
    [SerializeField] private AudioClip soundClip;
    [Range(0f, 1f)]
    [SerializeField] private float volume = 1f;

    [Header("Variación de Tono (Pitch)")]
    [Tooltip("Si está activo, elige un tono aleatorio entre Min y Max")]
    [SerializeField] private bool randomizePitch = false;
    [Range(0.1f, 3f)]
    [SerializeField] private float minPitch = 0.85f;
    [Range(0.1f, 3f)]
    [SerializeField] private float maxPitch = 1.15f;

    private bool _hasTriggered = false;

    private void Awake()
    {
        // Asegura que el collider esté configurado como Trigger
        Collider col = GetComponent<Collider>();
        if (col != null)
        {
            col.isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_hasTriggered && triggerOnlyOnce) return;

        if (other.CompareTag(targetTag))
        {
            PlayRemoteSound();

            if (triggerOnlyOnce)
            {
                _hasTriggered = true;
                // Desactiva el collider para evitar llamadas extra
                GetComponent<Collider>().enabled = false;
            }
        }
    }

    private void PlayRemoteSound()
    {
        if (remoteAudioSource == null)
        {
            Debug.LogWarning($"[DistanceSoundTrigger] en '{gameObject.name}' no tiene asignado un Remote AudioSource.", this);
            return;
        }

        // Configuración de pitch
        remoteAudioSource.pitch = randomizePitch ? Random.Range(minPitch, maxPitch) : 1f;

        // Reproducción con clip específico o el que ya tenga el AudioSource
        if (soundClip != null)
        {
            remoteAudioSource.PlayOneShot(soundClip, volume);
        }
        else if (remoteAudioSource.clip != null)
        {
            remoteAudioSource.volume = volume;
            remoteAudioSource.Play();
        }
        else
        {
            Debug.LogWarning($"[DistanceSoundTrigger] No hay ningún AudioClip asignado en el trigger ni en el AudioSource remoto.", this);
        }
    }

    // Dibuja una línea de referencia en la vista de escena para visualizar hacia dónde va el sonido
    private void OnDrawGizmosSelected()
    {
        if (remoteAudioSource != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(transform.position, remoteAudioSource.transform.position);
            Gizmos.DrawWireSphere(remoteAudioSource.transform.position, 1.5f);
        }
    }
}