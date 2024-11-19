using System;
using UnityEngine;

public enum EProjectile
{
    TestBullet,
    Bullet7mm, // 저격 라이플
    Bullet5mm // 권총
}


[Serializable]
public class ProjectileData
{
    public float speed = 100f;
    public float lifeTime = 3f;
    public AmmoType type;
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


    public void Initialize(Vector3 firePoint, Vector3 direction)
    {
        gameObject.SetActive(false);
        
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.position = firePoint;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    public virtual void Fire(Vector3 firePoint, Vector3 direction)
    {
        Initialize(firePoint, direction);

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
