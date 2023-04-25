using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Vector3 target;
    private Vector3 targetOld;
    [SerializeField] private Transform character;

    void FixedUpdate()
    {
        targetOld = target;
        target = Vector3.Lerp(target, character.transform.position, .1f);
    }

    void LateUpdate()
    {
        var t = (Time.time - Time.fixedTime) / Time.fixedDeltaTime;
        transform.position = Vector3.Lerp(targetOld, target, t);
        transform.position = transform.position._xy0() + Vector3.back * 500 + target._00z();
    }
}