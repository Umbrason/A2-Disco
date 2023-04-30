
using UnityEngine;

public class SineMovement : MonoBehaviour
{
    public float amplitude = 1f;
    public float offset = 0f;
    public Vector2 axis = Vector2.up;
    public float phaseDuration = 1f;

    void Update()
    {
        transform.localPosition = axis * (amplitude * Mathf.Sin(Time.time * Mathf.PI * 2f / phaseDuration) + offset);
    }
}
