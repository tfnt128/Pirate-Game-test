using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Behavior components")]
    [SerializeField] private float spawnRate;
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private GameObject[] enemyPrefab;
    [SerializeField] private bool canSpawn = true;
    
    [Header("Options Data")]
    public OptionsData optionsData;

    private void Start()
    {
        spawnRate = optionsData.enemySpawnRate;
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