using UnityEngine;

public class ZombieRunnerController : ZombieController
{
    [SerializeField]
    private float _speedMultiplier;

    protected override float SpeedModifier()
    {
        return base.SpeedModifier() * _speedMultiplier;
    }
}
