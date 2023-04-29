using UnityEngine;

public class LightSpawner : MonoBehaviour
{
    [SerializeField] private GameObject Prefab;
    [SerializeField] private Camera SpawnRegionCamera;

    public void Spawn()
    {
        var instance = Instantiate(Prefab);
        var min = SpawnRegionCamera.cameraToWorldMatrix.MultiplyPoint(Vector2.zero)._xy();
        var max = SpawnRegionCamera.cameraToWorldMatrix.MultiplyPoint(Vector2.one)._xy();
        instance.transform.position = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
    }
}
