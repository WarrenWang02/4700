using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorControlManager : MonoBehaviour
{
    [Range(0, 1)] public float rC = 1.0f; // red channel
    [Range(0, 1)] public float gC = 1.0f; // green channel
    [Range(0, 1)] public float bC = 1.0f; // blue channel
    [Range(0, 10)] public float gammaC = 1.0f; //gamma

    // Start is called before the first frame update
    void Start()
    {
        //targetLight = FindObjectOfType<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (targetLight != null)
        {
            targetLight.intensity = gammaC;
        }
        */
    }
}
