using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;

[RequireComponent(typeof(Collider))]
public class FreezearPlayer : MonoBehaviour
{
    [Header("Configuración del Trigger")]
    [Tooltip("Tag asignado al XR Origin o al objeto con el CharacterController")]
    [SerializeField] private string targetTag = "Player";
    [SerializeField] private bool triggerOnlyOnce = true;

    [Header("Locomotion Providers")]
    [Tooltip("Arrastra el componente de movimiento continuo (Continuous Move Provider)")]
    [SerializeField] private ContinuousMoveProvider moveProvider;

    [Tooltip("Arrastra el componente de giro (Snap Turn Provider o Continuous Turn Provider)")]
    [SerializeField] private ContinuousTurnProvider turnProvider;

    [Header("Restaurar Control (Opcional)")]
    [Tooltip("¿Devolver el control tras pasar unos segundos?")]
    [SerializeField] private bool restoreControlAfterDelay = false;
    [SerializeField] private float freezeDuration = 3f;

    private bool _hasTriggered = false;

    private void Awake()
    {
        Collider col = GetComponent<Collider>();
        if (col != null) col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_hasTriggered && triggerOnlyOnce) return;

        // Valida tanto el objeto que colisiona como su objeto raíz (XR Origin)
        if (other.CompareTag(targetTag) || other.transform.root.CompareTag(targetTag))
        {
            SetLocomotionState(false);

            if (restoreControlAfterDelay)
            {
                Invoke(nameof(RestoreLocomotion), freezeDuration);
            }

            if (triggerOnlyOnce)
            {
                _hasTriggered = true;
                GetComponent<Collider>().enabled = false;
            }
        }
    }

    public void SetLocomotionState(bool isEnabled)
    {
        if (moveProvider != null) moveProvider.enabled = isEnabled;
        if (turnProvider != null) turnProvider.enabled = isEnabled;
    }

    private void RestoreLocomotion()
    {
        SetLocomotionState(true);
    }
}