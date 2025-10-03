using UnityEngine;

public class LightBlink : MonoBehaviour
{
    
    private Light light;
    [SerializeField] private float minIntensity;
    [SerializeField] private float maxIntensity;
    
    [SerializeField] private float intensityChangeAmount;
    
    [SerializeField] private bool intensityChange;
    
    void Start()
    {
        light = GetComponent<Light>();
    }
    
    void Update()
    {
        
        if (!intensityChange)
        {
            light.intensity += Time.deltaTime * intensityChangeAmount;
        }
        else if (intensityChange)
        {
            light.intensity -= Time.deltaTime * intensityChangeAmount;
        }
        
        if(light.intensity >= maxIntensity) intensityChange = true;
        else if(light.intensity <= minIntensity) intensityChange = false;
    }
}
