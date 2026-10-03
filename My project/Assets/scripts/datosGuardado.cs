using UnityEngine;

public class datosGuardado : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string datos_encriptados = PlayerPrefs.GetString("datos", "404");

        Debug.Log(datos_encriptados);

        string datos_desencriptados = AESEncryption.Decrypt(datos_encriptados);

        Debug.Log(datos_desencriptados);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
