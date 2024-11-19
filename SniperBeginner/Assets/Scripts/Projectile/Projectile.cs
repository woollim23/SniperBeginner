using System;
using UnityEngine;

[Serializable]
public class ProjectileData
{
    public float damage = 50f;
    public float speed = 100f;
    public float lifeTime = 3f;
    public AmmoType type;
}

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    Rigidbody rigidBody;
    public ProjectileData data;

    protected virtual void Awake() 
    {
        rigidBody = GetComponent<Rigidbody>();
    }


    public void Initialize(Vector3 firePoint, Vector3 direction)
    {
        gameObject.SetActive(false);
        
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;

        transform.position = firePoint;
        transform.rotation = Quaternion.LookRotation(direction);
    }

    public virtual void Fire(Vector3 firePoint, Vector3 direction, float damage = 50f)
    {
        Initialize(firePoint, direction);

        gameObject.SetActive(true);

        rigidBody.AddForce(direction.normalized * data.speed, ForceMode.Impulse);

        if (IsInvoking("Release"))
            CancelInvoke("Release");
        
        Invoke("Release", data.lifeTime);
    }

    protected virtual void Release()
    {
        ObjectPoolManager.Instance.Release(data.type, this);
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (IsInvoking("Release"))
            CancelInvoke("Release");

        Release();
    }
}
