using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggerObj : MonoBehaviour
{
    [Header("Configuración del Disparador")]
    [Tooltip("Tag del objeto que activa el trigger (normalmente 'Player')")]
    [SerializeField] private string targetTag = "Player";
    [Tooltip("¿El trigger solo debe actuar una vez?")]
    [SerializeField] private bool triggerOnlyOnce = true;

    [Header("Objeto a Desactivar (Opcional)")]
    [Tooltip("Arrastra aquí el GameObject que deseas ocultar o apagar")]
    [SerializeField] private GameObject targetObject;

    [Header("Objetos a Activar (Opcionales)")]
    [Tooltip("Primer objeto a encender")]
    [SerializeField] private GameObject activateObject1;
    [Tooltip("Segundo objeto a encender")]
    [SerializeField] private GameObject activateObject2;

    [Header("Tiempos")]
    [Tooltip("Tiempo en segundos antes de ejecutar las acciones (0 = inmediato)")]
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
            if (delay > 0f)
            {
                Invoke(nameof(ExecuteActions), delay);
            }
            else
            {
                ExecuteActions();
            }

            if (triggerOnlyOnce)
            {
                _hasTriggered = true;
                GetComponent<Collider>().enabled = false;
            }
        }
    }

    private void ExecuteActions()
    {
        // Desactiva el objeto objetivo si está asignado
        if (targetObject != null)
        {
            targetObject.SetActive(false);
        }

        // Activa los objetos designados si están asignados
        if (activateObject1 != null)
        {
            activateObject1.SetActive(true);
        }

        if (activateObject2 != null)
        {
            activateObject2.SetActive(true);
        }
    }

    // Gizmos para visualizar las conexiones en la vista de Escena
    private void OnDrawGizmosSelected()
    {
        // Línea roja hacia el objeto que se apaga
        if (targetObject != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, targetObject.transform.position);
            Gizmos.DrawWireCube(targetObject.transform.position, Vector3.one * 0.4f);
        }

        // Líneas verdes hacia los objetos que se encienden
        Gizmos.color = Color.green;
        if (activateObject1 != null)
        {
            Gizmos.DrawLine(transform.position, activateObject1.transform.position);
            Gizmos.DrawWireSphere(activateObject1.transform.position, 0.3f);
        }

        if (activateObject2 != null)
        {
            Gizmos.DrawLine(transform.position, activateObject2.transform.position);
            Gizmos.DrawWireSphere(activateObject2.transform.position, 0.3f);
        }
    }
}