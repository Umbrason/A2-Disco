
using UnityEngine;

public class LightMatrix : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int height;
    [SerializeField] private float scale;

    [SerializeField] private GameObject LightPrefab;

    [SerializeField] private float lightScale;
    [SerializeField] private float randomness = 0f;

    void Start()
    {
        ShaderLightingEffect.screenMin = new Vector2(width, height) * scale / -2f;
        ShaderLightingEffect.screenMax = new Vector2(width, height) * scale / +2f;
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                var pos = new Vector2(x, y) * scale;
                var random = new Vector2(Random.Range(0, (float)width), Random.Range(0, (float)height)) * scale;
                pos = Vector2.Lerp(pos, random, randomness);
                pos -= new Vector2(width, height) / 2f * scale;

                var instance = Instantiate(LightPrefab, new Vector3(pos.x, pos.y, 0), Quaternion.identity);
                instance.transform.SetParent(transform);
                instance.transform.localScale = (Vector3)Vector2.one * scale * lightScale + Vector3.forward;
            }
    }
}
