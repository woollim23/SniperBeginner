using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public interface IDamagable
{
    void TakeDamage(float damage);
    void Die();
}