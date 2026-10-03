using System;
using UnityEngine;

public class Datos : MonoBehaviour
{
    string datos_texto;

    class jugador
    {
        public int vidas;
        public float energia;
        public int dienros;
        public infoJugador info_jugador;
    }
    [Serializable]
    class infoJugador
    {
        public string nombre;
        public string color_favorito;
        public int edad;

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        /*
        datos_texto = "{\"vidas\": 15, \"energia\": 30, \"dineros\": 300, \"info_jugador\" : {\r\n        \"nombre\" : \"Pedro123\",\r\n        \"color_favorito\" : \"verde\",\r\n        \"edad\" : 15,\r\n        \"cumpleanos\" : {\r\n            \"dia\" : 10,\r\n            \"mes\" : 4\r\n        }\r\n    }}";
        jugador objeto_jugador = JsonUtility.FromJson<jugador>(datos_texto);
        Debug.Log(objeto_jugador.info_jugador.nombre);
        */
        
        jugador objeto_jugador = new jugador();
        objeto_jugador.vidas = 4;
        objeto_jugador.energia = 50;
        objeto_jugador.dienros = 100;

        objeto_jugador.info_jugador = new infoJugador();
        objeto_jugador.info_jugador.nombre = "Ete sech";
        objeto_jugador.info_jugador.color_favorito = "verde me la muerde";
        objeto_jugador.info_jugador.edad = 18;


        Debug.Log(objeto_jugador.dienros);

        string dato_texto = JsonUtility.ToJson(objeto_jugador);

        string datos_encriptados = AESEncryption.Encrypt(dato_texto);

        Debug.Log(datos_encriptados);

        PlayerPrefs.SetString("datos", datos_encriptados);

        Debug.Log(dato_texto);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
