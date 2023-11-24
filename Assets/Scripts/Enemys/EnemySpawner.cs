using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private GameObject[] enemyPrefab;
    [SerializeField] private bool canSpawn = true;

    private void Start()
    {
        StartCoroutine(Spawner());
    }

    private WaitForSeconds _wait;
    private IEnumerator Spawner()
    {
        _wait = new WaitForSeconds(spawnRate);
         while (canSpawn)
         {
             yield return _wait;
             
             int randomEnemy = Random.Range(0, enemyPrefab.Length);
             int randomPos = Random.Range(0, spawnPositions.Length);
             GameObject enemyToSpawn = enemyPrefab[randomEnemy];
             Transform spawnPosition = spawnPositions[randomPos];

             Instantiate(enemyToSpawn, spawnPosition.position, Quaternion.identity);
         }
    }
}