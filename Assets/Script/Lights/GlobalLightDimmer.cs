using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GlobalLightDimmer : MonoBehaviour
{
    private float m_brightness = 1f;
    public float Brightness
    {
        get => m_brightness; set
        {
            GlobalLight.color = GlobalLight.color * (value / m_brightness);
            m_brightness = value;
        }
    }
    [SerializeField] private Light2D GlobalLight;

}
