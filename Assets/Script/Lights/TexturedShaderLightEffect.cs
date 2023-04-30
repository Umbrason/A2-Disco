using UnityEngine;

public class TexturedShaderLightEffect : ShaderLightingEffect, ILightingEffect
{
    [SerializeField] private Texture2D texture;

    void ILightingEffect.OnEnable()
    {
        var kernel = shader.FindKernel("lights");
        shader.SetTexture(kernel, "_Tex", texture);
    }
}
