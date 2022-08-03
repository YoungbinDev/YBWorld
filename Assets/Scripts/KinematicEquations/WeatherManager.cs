using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    public Wind wind;

    [Header("World")]
    [ReadOnly] public Vector3 worldAcceleration;

    [Header("Wind")]
    public Vector3 windAcceleration;

    public void Init()
    {
        wind = GetComponentInChildren<Wind>();
        windAcceleration = wind.acceleration;

        if (wind != null)
            worldAcceleration = Physics.gravity + windAcceleration;
    }

    private void Update()
    {
        if(wind != null)
            worldAcceleration = Physics.gravity + windAcceleration;
    }
}
