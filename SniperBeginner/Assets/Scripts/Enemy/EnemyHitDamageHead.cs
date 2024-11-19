using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitDamageHead : EnemyHitDamage
{
    public override void TakeDamage(float damage)
    {
        damage *= 10;
        base.TakeDamage(damage);
    }
}
