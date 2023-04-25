using UnityEngine;

public class DogeCheatCode : MonoBehaviour, ICheatCode
{
    public string Name => "doge";

    [SerializeField]
    private Sprite DogeSprite;

    public void Execute() => NPCSpriteOverrider.Toggle(DogeSprite);
}
