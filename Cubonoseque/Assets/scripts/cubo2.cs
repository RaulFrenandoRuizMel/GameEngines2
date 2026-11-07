using UnityEngine;

public class cubo2 : MonoBehaviour
{
    [SerializeField] AnimationCurve curve;

    float valor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        valor = 0;
    }

    // Update is called once per frame
    void Update()
    {
        valor += Time.deltaTime;
        if(valor > 1)
        {
            valor = 0;
        }
        this.transform.localScale = new Vector3(curve.Evaluate(valor), 1, 1);
    }
}
