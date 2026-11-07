using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class cube5 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            this.transform.DOMoveX(2, 2);
        }

        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            this.transform.DOMoveX(-2, 2);
        }
    }
}
