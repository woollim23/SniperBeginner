using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitDamageHeart : EnemyHitDamage
{
    public override void TakeDamage(float damage)
    {
        damage *= 5;
        base.TakeDamage(damage);
    }

    public override bool IsSnipable(float damage)
    {
        return enemy.Health <= damage * 5;
    }
}