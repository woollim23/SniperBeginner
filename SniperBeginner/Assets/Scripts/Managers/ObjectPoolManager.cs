using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    public Dictionary<AmmoType, ObjectPool<Projectile>> projectilePools = new Dictionary<AmmoType, ObjectPool<Projectile>>();
    private Dictionary<ParticleType, ObjectPool<GameObject>> particlePools = new Dictionary<ParticleType, ObjectPool<GameObject>>();

    public void AddProjectilePool(Projectile projectile, int initialSize = 31, int maxSize = 500)
    {
        projectilePools.Add(projectile.data.type, new ObjectPool<Projectile>(
            () => { return Instantiate(projectile); },
            null, //(projectile)=>{},
            (projectile) => { projectile.gameObject.SetActive(false); }, 
            (projectile) => { Destroy(projectile.gameObject); }, 
            false, initialSize, maxSize));
    }


    public Projectile Get(AmmoType type)
    {
        return projectilePools[type].Get();
    }

    public void Release(AmmoType type, Projectile projectile)
    {
        projectilePools[type].Release(projectile);
    }

    public void AddParticlePool(ParticleType type, GameObject prefab, int initialSize = 10, int maxSize = 50)
    {
        if (particlePools.ContainsKey(type)) return;

        particlePools[type] = new ObjectPool<GameObject>(
            () => Instantiate(prefab),
            (particle) => particle.SetActive(true),
            (particle) => particle.SetActive(false),
            (particle) => Destroy(particle),
            false, initialSize, maxSize);
    }

    public GameObject GetParticle(ParticleType type)
    {
        if (!particlePools.ContainsKey(type)) return null;
        return particlePools[type].Get();
    }

    public void ReleaseParticle(ParticleType type, GameObject particle)
    {
        if (particlePools.ContainsKey(type))
        {
            particlePools[type].Release(particle);
        }
    }
}