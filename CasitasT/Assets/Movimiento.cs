using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 8f;
    [SerializeField] private float acceleration = 12f;

    [Header("Jump & Gravity")]
    [SerializeField] private float jumpHeight = 1.2f;
    [SerializeField] private float gravity = -20f;

    [Header("Ground Check")]
    [SerializeField] private float groundedStickForce = -2f;

    private CharacterController controller;

    private Vector3 velocity;
    private Vector3 currentMovement;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        HandleMovement();
        HandleGravity();
    }

    private void HandleMovement()
    {
        Vector2 input = Vector2.zero;

        if (Keyboard.current != null)
        {
            input = Vector2.zero;

            if (Keyboard.current.aKey.isPressed)
                input.x -= 1;

            if (Keyboard.current.dKey.isPressed)
                input.x += 1;

            if (Keyboard.current.sKey.isPressed)
                input.y -= 1;

            if (Keyboard.current.wKey.isPressed)
                input.y += 1;

            input = Vector2.ClampMagnitude(input, 1f);
        }

        Vector3 direction =
            transform.right * input.x +
            transform.forward * input.y;

        bool sprinting =
            Keyboard.current != null &&
            Keyboard.current.leftShiftKey.isPressed;

        float targetSpeed = sprinting ? sprintSpeed : walkSpeed;

        Vector3 targetMovement = direction * targetSpeed;

        currentMovement = Vector3.MoveTowards(
            currentMovement,
            targetMovement,
            acceleration * Time.deltaTime
        );

        controller.Move(currentMovement * Time.deltaTime);

        HandleJump();
    }

    private void HandleJump()
    {
        if (controller.isGrounded)
        {
            if (velocity.y < 0)
                velocity.y = groundedStickForce;

            if (Keyboard.current != null &&
                Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }
    }

    private void HandleGravity()
    {
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}