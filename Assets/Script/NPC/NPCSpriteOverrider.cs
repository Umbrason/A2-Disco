using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class NPCSpriteOverrider : MonoBehaviour
{
    private static Sprite currentOverride = null;
    public static Sprite CurrentOverride
    {
        get => currentOverride;
        set {
            if (currentOverride == value) return;
            currentOverride = value;
            SetOverride?.Invoke(value);
        }
    }

    private static event Action<Sprite> SetOverride;

    private Sprite originalSprite;

    public static void Toggle(Sprite sprite) =>
        CurrentOverride = CurrentOverride == sprite ? null : sprite;

    void Start()
    {
        SetOverride += OnSetOverride;
        if (currentOverride != null) OnSetOverride(currentOverride);
    }

    void OnSetOverride(Sprite sprite)
    {
        var renderer = GetComponent<SpriteRenderer>();
        
        if (originalSprite == null)
            originalSprite = renderer.sprite;

        renderer.sprite = sprite == null ? originalSprite : sprite;
    }

    void OnDestroy()
    {
        SetOverride -= OnSetOverride;
    }
}
