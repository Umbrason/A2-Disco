using System.Collections.Generic;

public class GrandMA3
{
    public readonly static List<DiscoLight> lights = new();
    public static ILightingEffect ActiveEffect;

    void Update()
    {
        ActiveEffect?.UpdateLights(lights);
    }
}
