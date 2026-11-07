using UnityEngine;
using DG.Tweening;

public class cube3 : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Repetir();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Repetir()
    {
        this.transform.DOScaleX(2, 2).OnComplete(() =>
        {
            this.transform.DOScaleX(1, 2f).OnComplete(Repetir);
        });
    }
}
