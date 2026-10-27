using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggerUI : MonoBehaviour
{
    [Header("Configuración")]
    [Tooltip("Tag del jugador")]
    [SerializeField] private string targetTag = "Player";
    [Tooltip("¿Activar solo una vez?")]
    [SerializeField] private bool triggerOnlyOnce = true;

    [Header("Objetivo")]
    [Tooltip("Arrastra aquí el objeto que tiene el script TextoTerror")]
    [SerializeField] private TextoTerror targetTextoTerror;
    [SerializeField] private GameObject Txt_txt;

    [Header("Sobrescribir Texto (Opcional)")]
    [Tooltip("Si escribes algo aquí, dirá este texto en lugar del configurado por defecto")]
    [TextArea(2, 4)]
    [SerializeField] private string customTextOverride = "";

    private bool _hasTriggered = false;

    private void Awake()
    {
        Collider col = GetComponent<Collider>();
        if (col != null) col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Txt_txt.SetActive(true);
        if (_hasTriggered && triggerOnlyOnce) return;
        
      

        if (other.CompareTag(targetTag))
        {
            
            if (targetTextoTerror != null)
            {
                if (!string.IsNullOrEmpty(customTextOverride))
                {
                    targetTextoTerror.StartDialogue(customTextOverride);
                }
                else
                {
                    targetTextoTerror.StartDialogue();
                }

                if (triggerOnlyOnce)
                {
                    _hasTriggered = true;
                    GetComponent<Collider>().enabled = false;
                }
            }
            else
            {
                Debug.LogWarning($"[TriggerDialogoTerror] en '{gameObject.name}' no tiene asignado TextoTerror.", this);
            }
        }
    }
}