using UnityEngine;

public class Personaje : MonoBehaviour
{
    Transform componeteTransform;
    Vector3 posicion;
    Vector3 rotacion;
    public GameObject objetoModelo;
    Animator ComponenteAnimator;
    float velocidad_lineal;
    bool caminando;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        posicion = Vector3.zero;
        rotacion = Vector3.zero;
        componeteTransform = GetComponent<Transform>();
        ComponenteAnimator = objetoModelo.GetComponent<Animator>();
        velocidad_lineal = 1.5f;
        caminando = false;
    }

    // Update is called once per frame
    void Update()
    {
        caminando = false;

        if (!Input.GetKey(KeyCode.Z))
        {

            if (Input.GetKey(KeyCode.RightArrow))
            {
                caminando = true;
                posicion.x += velocidad_lineal * Time.deltaTime;
                rotacion.y = 0;
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                caminando = true;
                posicion.x -= velocidad_lineal * Time.deltaTime;
                rotacion.y = 180;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                caminando = true;
                posicion.z += velocidad_lineal * Time.deltaTime;
                rotacion.y = 270;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                caminando = true;
                posicion.z -= velocidad_lineal * Time.deltaTime;
                rotacion.y = 90;
            }

            if (caminando == true)
            {
                ComponenteAnimator.SetInteger("Estado", 1);
            }

            if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.UpArrow))
            {
                rotacion.y = 315;
            }
            if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.UpArrow))
            {
                rotacion.y = 225;
            }
            if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.DownArrow))
            {
                rotacion.y = 45;
            }
            if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.DownArrow))
            {
                rotacion.y = 115;
            }
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            velocidad_lineal = 3;
            if (caminando == true)
            {
                ComponenteAnimator.SetInteger("Estado", 2);
            }
            else
            {
                ComponenteAnimator.SetInteger("Estado", 0);

            }
        }
        else
        {
            if (caminando == true)
            {
                ComponenteAnimator.SetInteger("Estado", 1);
            }
            else
            {
                ComponenteAnimator.SetInteger("Estado", 0);
            }
            velocidad_lineal = 1.5f;
        }

        if (Input.GetKey(KeyCode.Z))
        {
            ComponenteAnimator.SetInteger("Estado", 3);
        }
        componeteTransform.position = posicion;
        componeteTransform.rotation = Quaternion.RotateTowards(componeteTransform.rotation, Quaternion.Euler(rotacion), Time.deltaTime * 400);
    }
}
