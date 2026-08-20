using UnityEngine;
using UnityEngine.InputSystem;

public class AnimacionManoDerecha : MonoBehaviour
{
    [SerializeField] RiggeadorManos manoDerecha;
    PlayerInput playerInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = this.GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        manoDerecha.gripSlider = playerInput.actions["XRI Right Interaction/Select Value"].ReadValue<float>();
        manoDerecha.triggerSlider = playerInput.actions["XRI Right Interaction/Activate Value"].ReadValue<float>();
        manoDerecha.botonAPresionado = playerInput.actions["XRI Right Interaction/BotonAFrontales"].IsPressed();

    }
}
