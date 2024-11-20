using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    [SerializeField] private List<GameObject> enemies;

    void Start()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            Debug.Log(enemies[i].GetComponentInChildren<Enemy>().Health + " " + enemies[i].transform.position);
        }
    }

    void Update()
    {
        
    }
}
