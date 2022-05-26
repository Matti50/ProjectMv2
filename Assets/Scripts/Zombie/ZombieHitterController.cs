using UnityEngine;

public class ZombieHitterController : ZombieController
{
    [SerializeField]
    private int _damageIncrease;

    protected override int DamageModifier()
    {
        return base.DamageModifier() + _damageIncrease;
    }
}
