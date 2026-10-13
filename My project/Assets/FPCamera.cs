using UnityEngine;
using UnityEngine.InputSystem;

public class FirstPersonCamera : MonoBehaviour
{
    [Header("Look Settings")]
    [SerializeField] private float sensitivity = 0.15f;
    [SerializeField] private float maxLookAngle = 89f;

    [Header("References")]
    [SerializeField] private Transform playerBody;

    private float verticalRotation = 0f;

    private void Awake()
    {
        // Si no asignaste Player Body manualmente,
        // usamos el padre de la cámara.
        if (playerBody == null)
        {
            playerBody = transform.parent;
        }
    }

    private void Start()
    {
        LockCursor();
    }

    private void Update()
    {
        HandleMouseLook();

        // ESC libera el mouse
        if (Keyboard.current != null &&
            Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Click izquierdo vuelve a capturarlo
        if (Mouse.current != null &&
            Mouse.current.leftButton.wasPressedThisFrame)
        {
            LockCursor();
        }
    }

    private void HandleMouseLook()
    {
        if (Mouse.current == null)
            return;

        if (Cursor.lockState != CursorLockMode.Locked)
            return;

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();

        float mouseX = mouseDelta.x * sensitivity;
        float mouseY = mouseDelta.y * sensitivity;

        // -------------------------
        // ARRIBA / ABAJO
        // -------------------------

        verticalRotation -= mouseY;

        verticalRotation = Mathf.Clamp(
            verticalRotation,
            -maxLookAngle,
            maxLookAngle
        );

        transform.localRotation =
            Quaternion.Euler(verticalRotation, 0f, 0f);

        // -------------------------
        // IZQUIERDA / DERECHA
        // -------------------------

        playerBody.Rotate(
            Vector3.up * mouseX
        );
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}