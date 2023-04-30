using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSharkCheatCode : MonoBehaviour, ICheatCode
{
    public string Name => "cshark";

    [SerializeField]
    private Sprite CSharkSprite;
    [SerializeField] private ShaderLightingEffect ShaderEffect;

    public void Execute()
    {
        NPCSpriteOverrider.Toggle(CSharkSprite);
        if (ShaderEffect) GrandMA3.SetEffect(ShaderEffect);
    }
}
