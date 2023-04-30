using System;
using UnityEngine;

public readonly struct TransformResetter : IDisposable
{
    private readonly Transform transform;
    private readonly Vector3 scaling;
    private readonly Quaternion rotation;

    public TransformResetter(Transform transform)
    {
        this.transform = transform;
        scaling = transform.localScale;
        rotation = transform.rotation;
    }

    public void Dispose()
    {
        if (transform != null)
        {
            transform.localScale = scaling;
            transform.rotation = rotation;
        }
    }
}