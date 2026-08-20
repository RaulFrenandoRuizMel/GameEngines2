using UnityEngine;
using UnityEngine.InputSystem;

public class AnimacionManoIzquierda : MonoBehaviour
{
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
        manoIzquierda.gripSlider = playerInput.actions["Select Value"].ReadValue<float>();
        manoIzquierda.triggerSlider = playerInput.actions["Activate Value"].ReadValue<float>();
        manoIzquierda.botonAPresionado = playerInput.actions["BotonAFrontales"].IsPressed();

    }
}
