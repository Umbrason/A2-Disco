using System;
using System.Collections.Generic;
using UnityEngine;

public class ShaderLightingEffect : MonoBehaviour, ILightingEffect
{
    [SerializeField] private ComputeShader shader;

    public static Vector2 screenMin;
    public static Vector2 screenMax;        

    const int BATCHSIZE = 500;

    private class ComputeResource
    {
        public ComputeBuffer positionBuffer = new ComputeBuffer(BATCHSIZE, sizeof(float) * 2);
        public ComputeBuffer colorBuffer = new ComputeBuffer(BATCHSIZE, sizeof(float) * 3);

        internal void Dispose()
        {
            positionBuffer.Dispose();
            colorBuffer.Dispose();
        }
    }
    private readonly Queue<ComputeResource> availableResources = new();
    private readonly Queue<ComputeResource> usedResources = new();

    public void UpdateLights(List<DiscoLight> lights)
    {
        var lightCount = lights.Count;
        if (lightCount == 0) return;

        var kernel = shader.FindKernel("lights");
        shader.SetVector("iResolution", screenMax - screenMin);
        shader.SetFloat("iTime", Time.time);
        var lightArray = lights.ToArray();

        int offset = 0;
        while (offset < lightCount)
        {
            var resource = availableResources.Count > 0 ? availableResources.Dequeue() : new();
            usedResources.Enqueue(resource);

            var count = Mathf.Min(lightCount - offset, BATCHSIZE);

            var positions = new Vector2[count];

            for (int i = 0; i < count; i++)
                positions[i] = lights[i + offset].transform.position._xy() - screenMin;

            resource.positionBuffer.SetData(positions);
            shader.SetBuffer(kernel, "_Positions", resource.positionBuffer);
            shader.SetBuffer(kernel, "_Colors", resource.colorBuffer);
            shader.Dispatch(kernel, count, 1, 1);

            var colors = new Vector3[count];
            resource.colorBuffer.GetData(colors);

            for (int i = 0; i < count; i++)
            {
                var rgb = colors[i];
                lightArray[i + offset].Color = new Color(rgb.x, rgb.y, rgb.z, 1);
            }
            offset += BATCHSIZE;
        }
        while (usedResources.Count > 0)
            availableResources.Enqueue(usedResources.Dequeue());
    }

    void OnDestroy()
    {
        while (availableResources.Count > 0)
            availableResources.Dequeue().Dispose();
    }
}
