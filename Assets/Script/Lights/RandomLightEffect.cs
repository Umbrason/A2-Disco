using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class RandomLightEffect : ILightingEffect
{
    public float changeFrequency = 1f;

    public void UpdateLights(List<DiscoLight> lights)
    {
        var affected = lights.Where(l => Random.Range(0, 1f / changeFrequency) / Time.deltaTime <= 1).ToArray();
        foreach (var light in affected)
        {
            light.Color = Random.ColorHSV(0, 1, 1, 1, 1, 1);
        }
    }
}
