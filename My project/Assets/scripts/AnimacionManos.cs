using UnityEngine;
using UnityEngine.InputSystem;

public class AnimacionManos : MonoBehaviour
{
    [SerializeField] RiggeadorManos manoDerecha;
    [SerializeField] RiggeadorManos manoIzquierda;
    PlayerInput playerInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = this.GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        manoDerecha.gripSlider = playerInput.actions["Select Value Izquierda"].ReadValue<float>();
        manoDerecha.triggerSlider = playerInput.actions["Activate Value Izquierda"].ReadValue<float>();
        manoDerecha.botonAPresionado = playerInput.actions["BotonAFrontales Izquierda"].IsPressed();

        manoIzquierda.gripSlider = playerInput.actions["Select Value Derecha"].ReadValue<float>();
        manoIzquierda.triggerSlider = playerInput.actions["Activate Value Derecha"].ReadValue<float>();
        manoIzquierda.botonAPresionado = playerInput.actions["BotonAFrontales Derecha"].IsPressed();

    }
}
