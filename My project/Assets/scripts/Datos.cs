using System;
using System.Collections;
using UnityEngine;

public class Datos : MonoBehaviour
{
    string datos_texto;

    class jugador
    {
        public int vidas;
        public float energia;
        public int dinero;
        public infoJugador info_jugador;
        public Items[] inventario; 
    }
    [Serializable]
    class infoJugador
    {
        public string nombre;
        public string color_favorito;
        public int edad;

    }
    [Serializable]

    class Items
    {
        public string nombre_items;
        public string descripcion;
        public int cantidad;
        public Costo costo;
    }
    [Serializable]

    public class Costo
    {
        public int compra;
        public int venta;
    }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
    {
        /*
        datos_texto = "{\"vidas\" : 15,\r\n    \"energia\" : 30,\r\n    \"dinero\" : 300,\r\n \r\n    \"info_jugador\" : {\r\n        \"nombre\" : \"Pedro123\",\r\n        \"color_favorito\" : \"verde\",\r\n        \"edad\" : 15,\r\n        \"cumpleanos\" : {\r\n            \"dia\" : 10,\r\n            \"mes\" : 4\r\n        }\r\n    },\r\n \r\n    \"inventario\" : [\r\n        {\r\n            \"nombre_item\" : \"Espada de fuego\",\r\n            \"descripcion\" : \"Espada forjada por los elfos nativos del fuego\",\r\n            \"cantidad\" : 1,\r\n            \"costco\" : {\r\n                \"compra\" : 300,\r\n                \"venta\" : 340\r\n            }\r\n        },\r\n        {\r\n            \"nombre_item\" : \"Arco\",\r\n            \"descripcion\" : \"Arco para disparar flechas\",\r\n            \"cantidad\" : 2,\r\n            \"costco\" : {\r\n                \"compra\" : 90,\r\n                \"venta\" : 110\r\n            }\r\n        },\r\n        {\r\n            \"nombre_item\" : \"Flechas de cuerno de unicornio\",\r\n            \"descripcion\" : \"Flechas hechas con cuernos de unicornio cazados duarnte la primavera por enanos salvajes.\",\r\n            \"cantidad\" : 500,\r\n            \"costco\" : {\r\n                \"compra\" : 20,\r\n                \"venta\" : 30\r\n            }\r\n        }\r\n    ]}";
        jugador objeto_jugador = JsonUtility.FromJson<jugador>(datos_texto);
        Debug.Log(objeto_jugador.info_jugador.nombre);
        */

        /*
        
        jugador objeto_jugador = new jugador();
        objeto_jugador.vidas = 4;
        objeto_jugador.energia = 50;
        objeto_jugador.dinero = 100;

        objeto_jugador.info_jugador = new infoJugador();
        objeto_jugador.info_jugador.nombre = "Ete sech";
        objeto_jugador.info_jugador.color_favorito = "verde me la muerde";
        objeto_jugador.info_jugador.edad = 18;

        objeto_jugador.inventario = new Items[2];
        objeto_jugador.inventario[0] = new Items();
        objeto_jugador.inventario[0].nombre_items = "Dwayne";
        objeto_jugador.inventario[0].descripcion = "Es un mamón";
        objeto_jugador.inventario[0].cantidad = 7;
        objeto_jugador.inventario[0].costo = new Costo();
        objeto_jugador.inventario[0].costo.compra = 1;
        objeto_jugador.inventario[0].costo.venta = 2;

        objeto_jugador.inventario[0] = new Items();
        objeto_jugador.inventario[0].nombre_items = "Carta Pokemon";
        objeto_jugador.inventario[0].descripcion = "Es una carta toda dura";
        objeto_jugador.inventario[0].cantidad = 7;
        objeto_jugador.inventario[0].costo = new Costo();
        objeto_jugador.inventario[0].costo.compra = 6;
        objeto_jugador.inventario[0].costo.venta = 9;

        string dato_texto = JsonUtility.ToJson(objeto_jugador);

        Debug.Log(dato_texto);

        /*
        string datos_encriptados = AESEncryption.Encrypt(dato_texto);

        Debug.Log(datos_encriptados);

        PlayerPrefs.SetString("datos", datos_encriptados);
        */
        //Debug.Log(dato_texto);
        StartCoroutine(MiFuncion(24));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator MiFuncion(int valor)
    {
        Debug.Log("Hola");
        yield return new WaitForSeconds(6);
        Debug.Log("Adios" + valor);
        yield return new WaitForSeconds(7);
        Debug.Log("Y volví");

        yield return 0;
    }
}

