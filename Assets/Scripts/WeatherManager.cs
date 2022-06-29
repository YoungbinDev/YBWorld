using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    public Wind wind;

    public Vector3 worldAcceleration;

    public void Init()
    {
        wind = GetComponentInChildren<Wind>();

        worldAcceleration = Physics.gravity + wind.acceleration;
    }
}
