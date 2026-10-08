using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class conectarAPI : MonoBehaviour
{
    string texto_endpoint = "http://localhost:3000/";
    [SerializeField] TMP_Text textoGet;

    [SerializeField] TMP_InputField inputNombre;
    [SerializeField] TMP_InputField inputPrecio;
    [SerializeField] TMP_Dropdown inputMarca;



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

    enum Marcas
    {
        Doritos,Takis, Chetos, Sabritas, Chips
    }
    //GET


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inputMarca.ClearOptions();
        List<string> listaOpciones = new List<string>();
        for(int i = 0; i < 5; i++)
        {
            listaOpciones.Add(((Marcas)i).ToString());
        }

        inputMarca.AddOptions(listaOpciones);
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
        textoGet.text = "";
        foreach(Papitas papitas in frituras.papitas)
        {
            textoGet.text += "---------------\nMarca: " + papitas.marca 
                + "---------------\nNOmbre: " + papitas.nombre 
                + "---------------\nPrecio: " + papitas.precio;
        }
        //textoGet.text = WebRequest.downloadHandler.text;
    }

    public void CrearPapitas()
    {
        Papitas papitas = new Papitas();
        papitas.nombre = inputNombre.text;
        papitas.precio = int.Parse(inputPrecio.text);
        papitas.marca = ((Marcas)inputMarca.value).ToString();

        string texto_papitas = JsonUtility.ToJson(papitas);

        Debug.Log(texto_papitas);
    }

    IEnumerator postPapitas()
    {
        UnityWebRequest webRequest = UnityWebRequest.Post(texto_endpoint, "{}", "application/json");
        yield return webRequest.SendWebRequest();
    }
}
