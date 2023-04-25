using UnityEngine;

[RequireComponent(typeof(VelocityController))]
public class SprintController : MonoBehaviour
{
    private VelocityController cached_VC;
    private VelocityController VC => cached_VC ??= GetComponent<VelocityController>();

    private int speedStage;
    const int maxSpeed = 3;

    public void NextSpeed()
    {
        speedStage++;
        speedStage %= maxSpeed;
    }

    void FixedUpdate()
    {
        VC.AddAtomicSlowdownEffect(1 << speedStage, 1);
    }
}
