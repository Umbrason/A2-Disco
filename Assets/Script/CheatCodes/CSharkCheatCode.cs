using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSharkCheatCode : MonoBehaviour, ICheatCode
{
    public string Name => "cshark";

    [SerializeField]
    private Sprite CSharkSprite;

    public void Execute() => NPCSpriteOverrider.Toggle(CSharkSprite);
}
