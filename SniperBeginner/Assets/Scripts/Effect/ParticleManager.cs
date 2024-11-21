using UnityEngine;
using System.Collections;

public class ParticleManager : Singleton<ParticleManager>
{
    [Header("Particle Prefabs")]
    [SerializeField] private GameObject muzzleFlashPrefab;
    [SerializeField] private GameObject wallImpactPrefab;
    [SerializeField] private GameObject bloodImpactPrefab;

    private void Start()
    {
        ObjectPoolManager.Instance.AddParticlePool(ParticleType.MuzzleFlash, muzzleFlashPrefab);
        ObjectPoolManager.Instance.AddParticlePool(ParticleType.WallImpact, wallImpactPrefab);
        ObjectPoolManager.Instance.AddParticlePool(ParticleType.BloodImpact, bloodImpactPrefab);
    }

    public void SpawnMuzzleFlash(Transform muzzlePoint)
    {
        GameObject muzzleFlash = ObjectPoolManager.Instance.GetParticle(ParticleType.MuzzleFlash);
        if (muzzleFlash != null)
        {
            muzzleFlash.transform.position = muzzlePoint.position;
            muzzleFlash.transform.rotation = muzzlePoint.rotation;

            StartCoroutine(ReturnParticleAfterDelay(ParticleType.MuzzleFlash, muzzleFlash, 1.5f));
        }
    }

    public void SpawnWallImpact(Vector3 position, Vector3 normal)
    {
        GameObject wallImpact = ObjectPoolManager.Instance.GetParticle(ParticleType.WallImpact);
        if (wallImpact != null)
        {
            wallImpact.transform.position = position;
            wallImpact.transform.rotation = Quaternion.LookRotation(normal);

            StartCoroutine(ReturnParticleAfterDelay(ParticleType.WallImpact, wallImpact, 1.5f));
        }
    }

    public void SpawnBloodImpact(Vector3 position, Vector3 normal)
    {
        GameObject bloodImpact = ObjectPoolManager.Instance.GetParticle(ParticleType.BloodImpact);
        if (bloodImpact != null)
        {
            bloodImpact.transform.position = position;
            bloodImpact.transform.rotation = Quaternion.LookRotation(normal);

            StartCoroutine(ReturnParticleAfterDelay(ParticleType.BloodImpact, bloodImpact, 1.5f));
        }
    }

    IEnumerator ReturnParticleAfterDelay(ParticleType type, GameObject particle, float delay)
    {
        yield return new WaitForSeconds(delay);

        ObjectPoolManager.Instance.ReleaseParticle(type, particle);
    }
}