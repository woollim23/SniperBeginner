using System;
using UnityEngine;

[Serializable]
public class EnemyGroundData
{
    [field: SerializeField][field: Range(0f, 25f)] public float BaseSpeed { get; set; }
    [field: SerializeField][field: Range(0f, 25f)] public float BaseRotationDamping { get; private set; }
    [field: SerializeField] public float MinWanderDistance { get; private set; }
    [field: SerializeField] public float MaxWanderDistance { get; private set; }
}

[CreateAssetMenu(fileName = "Enemy", menuName = "Characters/Enemy")]
public class EnemySO : ScriptableObject
{
    [field: SerializeField] public float MaxHealth { get; private set; }
    [field: SerializeField] public int Damage;
    [field: SerializeField] public float PlayerChasingRange { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public float RateAttackTime { get; private set; }
    [field: SerializeField][field: Range(-10f, 10f)] public float Force { get; private set; }
    [field: SerializeField] [field: Range(0f, 1f)] public float Dealing_Start_TransitionTime { get; private set; }
    [field: SerializeField] [field: Range(0f, 1f)] public float Dealing_End_TransitionTime { get; private set; }
    [field: SerializeField] public EnemyGroundData GroundData { get; private set; }
}
