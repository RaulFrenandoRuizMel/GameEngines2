using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class cube4 : MonoBehaviour
{
    Vector3 rotacion;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rotacion = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            this.transform.DORotate(new Vector3(0, 0, 0), 0.5f);
            //rotacion.y = 0;
        }
        if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            this.transform.DORotate(new Vector3(0, 180, 0), 0.5f).SetDelay(2);

            //   rotacion.y = 180;
        }
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            this.transform.DORotate(new Vector3(0, 90, 0), 0.5f);

            // rotacion.y = 90;
        }
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            this.transform.DORotate(new Vector3(0, 270, 0), 0.5f);

            //rotacion.y = 270;
        }
        //this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.Euler(rotacion), 400 * Time.deltaTime);
    }
}
