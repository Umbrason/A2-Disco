using System.Collections.Generic;
using UnityEngine;

public class ShaderLightEffect : MonoBehaviour, ILightingEffect
{
    private ComputeShader shader;
    public void UpdateLights(IEnumerable<DiscoLight> lights)
    {
        
    }
}