
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D), typeof(SpriteRenderer))]
public class DiscoLight : MonoBehaviour
{
    [SerializeField] private Light2D Light;
    [SerializeField] private SpriteRenderer SR;

    void Awake() => GrandMA3.lights.Add(this);

    private Color m_Color;
    public Color Color
    {
        get => m_Color; set
        {
            m_Color = value;
            SR.color = value;
            Light.color = value;
        }
    }
}
