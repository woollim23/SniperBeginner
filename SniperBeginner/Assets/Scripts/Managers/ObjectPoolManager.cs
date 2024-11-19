using System.Collections.Generic;
using UnityEngine.Pool;

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    public Dictionary<AmmoType, ObjectPool<Projectile>> projectilePools = new Dictionary<AmmoType, ObjectPool<Projectile>>();

    public void AddProjectilePool(Projectile projectile, int initialSize = 31, int maxSize = 100)
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

}