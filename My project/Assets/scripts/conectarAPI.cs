using System;
using System.Collections;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class conectarAPI : MonoBehaviour
{
    string texto_endpoint = "http://localhost:3000/";
    [SerializeField] TMP_Text textoGet;
    [Serializable]
    class Papitas
    {
        public string nombre;
        public int precio;
        public string marca;
    }

    class Frituras
    {
        public Papitas[] papitas;
    }
    //GET


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConsultarPapitas()
    {
        StartCoroutine(GetPapitas());
    }

    IEnumerator GetPapitas()
    {
        UnityWebRequest WebRequest = UnityWebRequest.Get(texto_endpoint);
        yield return WebRequest.SendWebRequest();
        Debug.Log(WebRequest.downloadHandler.text);

        Frituras frituras = JsonUtility.FromJson<Frituras>(WebRequest.downloadHandler.text);
        Debug.Log(frituras.papitas[0].nombre);
        
        //textoGet.text = WebRequest.downloadHandler.text;
    }
}
