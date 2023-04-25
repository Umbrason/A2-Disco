using UnityEngine;

public class NinjaCheatCode : MonoBehaviour, ICheatCode
{
    public string Name => "ninja";

    [SerializeField]
    private NinjaMode NinjaMode;

    public void Execute() => NinjaMode.Toggle();
}
