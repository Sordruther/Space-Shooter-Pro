using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{   
    [SerializeField]
    private GameObject enemyContainer;
    public GameObject enemyPrefab;
    public GameObject[] powerUps;
    [SerializeField]
    float enemySpawnTime = 5f;
    float powerUpSpawnTime = 5f;

    bool stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemySpawnRoutine());
        StartCoroutine(PowerUpSpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator EnemySpawnRoutine()
    {
        while(stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8, 8f), 7, 0);
            GameObject newEnemy = Instantiate(enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = enemyContainer.transform;
            yield return new WaitForSeconds(enemySpawnTime);
        }
    }

    IEnumerator PowerUpSpawnRoutine()
    {
        while(stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8, 8f), 7, 0);
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(powerUps[randomPowerUp], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(powerUpSpawnTime);
        }
    }

    public void OnPlayerDeath()
    {
        stopSpawning = true;
    }
}
