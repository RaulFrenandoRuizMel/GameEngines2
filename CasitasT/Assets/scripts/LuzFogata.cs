
using UnityEngine;

public class LuzFogata : MonoBehaviour
{
    public Light fireLight;
    public float minIntensity = 1.5f;
    public float maxIntensity = 3f;
    public float speed = 5f;

    void Start()
    {
        if (fireLight == null)
            fireLight = GetComponent<Light>();
    }

    void Update()
    {
        if (fireLight != null)
        {
            float flicker = Mathf.PerlinNoise(Time.time * speed, 0f);
            fireLight.intensity = Mathf.Lerp(minIntensity, maxIntensity, flicker);
        }
    }
}
