using UnityEngine;
using UnityEngine.InputSystem;

public class Jugador : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.upArrowKey.wasPressedThisFrame);
        {
            this.transform.position += Vector3.up;
        }
        if (Keyboard.current.downArrowKey.wasPressedThisFrame) ;
        {
            this.transform.position += Vector3.down;
        }
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame) ;
        {
            this.transform.position += Vector3.right;
        }
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame) ;
        {
            this.transform.position += Vector3.left;
        }

    }
}
