using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ObtenerPokemon : MonoBehaviour
{
    class Pokemon
    {
        public int id;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(ConsultarPokemon());
    }
    IEnumerator ConsultarPokemon()
    {
        string url = "http://localhost:3000/";

        UnityWebRequest webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();

        if(webRequest.result == UnityWebRequest.Result.Success)
        {
            Pokemon pokemon = JsonUtility.FromJson<Pokemon>(webRequest.downloadHandler.text);
            Debug.Log(pokemon.id);

        }
        else
        {
            Debug.Log("Mamaste, 404");
        }

        /*Debug.Log(webRequest.result);
        Debug.Log(webRequest.downloadHandler.text);*/


        yield return 0;
    }
    // Update is called once per frame
    void Update()
    {
        
    }


}
