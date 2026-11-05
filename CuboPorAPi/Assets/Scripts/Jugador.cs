using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Networking;


public class Jugador : MonoBehaviour
{
    float contador;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        contador = 0;
    }

    // Update is called once per frame
    void Update()
    {

        contador += Time.deltaTime;
        if (contador > 0.1f)
        {
            contador = 0;
            StartCoroutine(ObtenerInput());
        }

        /*if (Keyboard.current.upArrowKey.wasPressedThisFrame)
        {
            this.transform.position += Vector3.up;
        }
        if (Keyboard.current.downArrowKey.wasPressedThisFrame)
        {
            this.transform.position += Vector3.down;
        }
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
        }
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            this.transform.position += Vector3.left;
        }*/

    }
    IEnumerator ObtenerInput()
    {
        UnityWebRequest webRequest = UnityWebRequest.Get("http://localhost:3000/");
        yield return webRequest.SendWebRequest();
        Debug.Log(webRequest.downloadHandler.text);

        switch(int.Parse(webRequest.downloadHandler.text))
        {
            case 1:
                this.transform.position += Vector3.up;
                break;
            case 2:
                this.transform.position += Vector3.down;
                break;
            case 3:
                this.transform.position += Vector3.left;
                break;
            case 4:
                this.transform.position += Vector3.right;
                break;
        }

    }
}
