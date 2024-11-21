using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    private Transform[] spawnPoints;

    void Start()
    {
        spawnPoints = GetComponentsInChildren<Transform>();

        SpawnItemsAtPoints();
    }

    private void SpawnItemsAtPoints()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            if (spawnPoint != this.transform)
            {
                ItemDropManager.Instance.DropRandomItem(spawnPoint.position);
            }
        }
    }
}