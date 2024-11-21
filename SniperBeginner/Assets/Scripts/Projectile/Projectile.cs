using System;
using UnityEngine;

[Serializable]
public class ProjectileData
{
    public float speed = 100f;
    public float lifeTime = 3f;
    public AmmoType type;
    public float damage = 0;
}

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    Rigidbody rigidBody;
    public string shooterTag;
    public ProjectileData data;

    [SerializeField] GameObject mesh;
    [SerializeField] GameObject trail;

    protected virtual void Awake() 
    {
        rigidBody = GetComponent<Rigidbody>();
    }


    public void Initialize(Vector3 firePoint, Vector3 direction)
    {
        gameObject.SetActive(false);

        mesh.SetActive(false);
        trail.SetActive(true);
        
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;

        transform.position = firePoint;
        transform.rotation = Quaternion.LookRotation(direction);

        gameObject.SetActive(true);
    }

    public void InitializeForCinemachine(Vector3 firePoint, Vector3 direction)
    {
        gameObject.SetActive(false);

        mesh.SetActive(true);
        trail.SetActive(false);

        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;

        transform.position = firePoint;
        transform.rotation = Quaternion.LookRotation(direction);

        gameObject.SetActive(true);
    }

    public virtual void Fire (Vector3 firePoint, Vector3 direction, float damage = 30f, string shooter = "Enemy")
    {
        Initialize(firePoint, direction);

        data.damage = damage;
        shooterTag = shooter;

        rigidBody.AddForce(direction.normalized * data.speed, ForceMode.Impulse);

        if (IsInvoking("Release"))
            CancelInvoke("Release");
        
        Invoke("Release", data.lifeTime);
    }

    public virtual void Release()
    {
        ObjectPoolManager.Instance.Release(data.type, this);
    }

    private void OnCollisionEnter(Collision other) 
    {
        if (IsSuicide(other.collider.tag))
            return;

        if (other.gameObject.TryGetComponent(out IDamagable damagable))
            damagable.TakeDamage(data.damage);


        ContactPoint contact = other.contacts[0];

        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Enemy"))
            ParticleManager.Instance.SpawnBloodImpact(contact.point, contact.normal);
        else
            ParticleManager.Instance.SpawnWallImpact(contact.point, contact.normal);

        if (IsInvoking("Release"))
            CancelInvoke("Release");

        Release();
    }

    bool IsSuicide(string victimTag)
    {
        return victimTag.Equals(shooterTag);
    }
}
