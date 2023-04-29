using UnityEngine;

public class LightSpawner : MonoBehaviour
{
    [SerializeField] private GameObject Prefab;
    [SerializeField] private Camera SpawnRegionCamera;

    public void Spawn()
    {
        var instance = Instantiate(Prefab);
        float halfw = SpawnRegionCamera.orthographicSize;
        float halfh = SpawnRegionCamera.orthographicSize / SpawnRegionCamera.aspect;
        var extends = new Vector2(halfw, halfh) * 2f;
        var min = SpawnRegionCamera.transform.position._xy() - extends;
        var max = SpawnRegionCamera.transform.position._xy() + extends;
        instance.transform.position = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
    }
}
