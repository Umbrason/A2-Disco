
using UnityEngine;

public class ShaderLightsCheatcode : MonoBehaviour, ICheatCode
{
    [field: SerializeField] public string Name { get; set; }
    public ShaderLightingEffect ShaderEffect;

    public void Execute()
    {
        if (ShaderEffect) GrandMA3.SetEffect(ShaderEffect).IWillNotClose();
    }
}
