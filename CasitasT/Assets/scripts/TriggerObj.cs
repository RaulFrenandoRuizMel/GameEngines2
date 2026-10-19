using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggerObj : MonoBehaviour
{
    [Header("Configuración del Disparador")]
    [Tooltip("Tag del objeto que activa el trigger (normalmente 'Player')")]
    [SerializeField] private string targetTag = "Player";
    [Tooltip("¿El trigger solo debe actuar una vez?")]
    [SerializeField] private bool triggerOnlyOnce = true;

    [Header("Objeto a Desactivar")]
    [Tooltip("Arrastra aquí el GameObject que deseas ocultar o apagar")]
    [SerializeField] private GameObject targetObject;

    [Tooltip("Tiempo en segundos antes de apagar el objeto (0 = inmediato)")]
    [SerializeField] private float delay = 0f;

    private bool _hasTriggered = false;

    private void Awake()
    {
        // Asegura que el collider funcione como trigger
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
            if (targetObject == null)
            {
                Debug.LogWarning($"[TriggerDeactivateObject] en '{gameObject.name}' no tiene asignado un 'targetObject'.", this);
                return;
            }

            if (delay > 0f)
            {
                Invoke(nameof(DeactivateTarget), delay);
            }
            else
            {
                DeactivateTarget();
            }

            if (triggerOnlyOnce)
            {
                _hasTriggered = true;
                GetComponent<Collider>().enabled = false;
            }
        }
    }

    private void DeactivateTarget()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(false);
        }
    }

    // Dibuja una línea en la vista de escena para identificar qué objeto apagará este trigger
    private void OnDrawGizmosSelected()
    {
        if (targetObject != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, targetObject.transform.position);
            Gizmos.DrawWireCube(targetObject.transform.position, Vector3.one * 0.5f);
        }
    }
}   