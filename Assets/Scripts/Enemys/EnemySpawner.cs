using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private bool canSpawn = true;

    [Header("Options Data")]
    [SerializeField] private OptionsData optionsData;

    private float _spawnRate;
    private WaitForSeconds _wait;

    private void Start()
    {
        InitializeSpawnRate();
        StartCoroutine(Spawner());
    }

    private void InitializeSpawnRate()
    {
        _spawnRate = optionsData.enemySpawnRate;
    }

    private IEnumerator Spawner()
    {
        _wait = new WaitForSeconds(_spawnRate);

        while (canSpawn)
        {
            yield return _wait;

            SpawnRandomEnemy();
        }
    }

    private void SpawnRandomEnemy()
    {
        int randomEnemyIndex = Random.Range(0, enemyPrefabs.Length);
        int randomSpawnIndex = Random.Range(0, spawnPositions.Length);

        GameObject enemyToSpawn = enemyPrefabs[randomEnemyIndex];
        Transform spawnPosition = spawnPositions[randomSpawnIndex];

        Instantiate(enemyToSpawn, spawnPosition.position, Quaternion.identity);
    }
}