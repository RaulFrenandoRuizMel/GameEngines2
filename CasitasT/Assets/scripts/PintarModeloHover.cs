using UnityEngine;

public class PintarModeloHover : MonoBehaviour
{
    [SerializeField] GameObject ModeloOriginal;
    [SerializeField] GameObject ModeloHover;

    bool agarrado;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ModeloHover.SetActive(false);
        ModeloOriginal.SetActive(true);
        agarrado = true;
    }
    
    public void HoverModelo()
    {
        if (agarrado) return;
        ModeloHover.SetActive(true);
        ModeloOriginal.SetActive(false);
    }

    public void UnHoverModelo()
    {
        ModeloHover.SetActive(false);
        ModeloOriginal.SetActive(true);
    }

    public void AgarrarModelo()
    {
        agarrado = true;
    }

    public void SoltarModelo()
    {
        agarrado = false;
    }
}
