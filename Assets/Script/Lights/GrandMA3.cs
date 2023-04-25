using System.Collections.Generic;
using UnityEngine;

public class GrandMA3 : MonoBehaviour
{
    public readonly static List<DiscoLight> lights = new();
    public static ILightingEffect ActiveEffect;

    void Update()
    {
        ActiveEffect?.UpdateLights(lights);
    }
}
