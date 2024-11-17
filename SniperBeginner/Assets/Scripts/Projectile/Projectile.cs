using System;
using UnityEngine;

public enum EProjectile
{
    TestBullet,
}


[Serializable]
public class ProjectileData
{
    public float speed = 100f;
    public float lifeTime = 3f;
    public EProjectile type;
}

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    Rigidbody rb;
    
    public ProjectileData data;

    protected virtual void Awake() 
    {
        rb = GetComponent<Rigidbody>();
    }

    public virtual void Fire(Vector3 firePoint, Vector3 direction)
    {
        rb.velocity = Vector3.zero;

        transform.position = firePoint;
        transform.rotation = Quaternion.LookRotation(direction);

        gameObject.SetActive(true);

        rb.AddForce(direction.normalized * data.speed, ForceMode.Impulse);

        if (IsInvoking("Release"))
            CancelInvoke("Release");
        
        Invoke("Release", data.lifeTime);
    }

    protected virtual void Release()
    {
        ObjectPoolManager.Instance.Release(data.type, this);
    }
}
