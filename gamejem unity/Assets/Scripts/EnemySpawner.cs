using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy {
    public GameObject prefab;
    public float difficultyValue;
}

public class EnemySpawner : Singleton<EnemySpawner>
{
    public Enemy[] spawnables;
    [SerializeField] float difficulty;
    [SerializeField] float nextWaveNeededDifficulty;
    [SerializeField] float difficultyMultiplier;
    [SerializeField] float timeBetweenWaves;
    [SerializeField] float minSpawnDistanceFromPlayer;
    [SerializeField] float maxSpawnDistanceFromPlayer;

    public int enemyCount;
    public int waveCount = 1;
    GameObject[] enemies;

    bool newWaveStarted = false;

    void Start() {

    }

    void Update() {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = enemies.Length;

        if (enemyCount == 0 && !newWaveStarted) {
            StartCoroutine("StartWave");
        }        
    }

    IEnumerator StartWave() {
        newWaveStarted = true;
        yield return new WaitForSeconds(timeBetweenWaves);
        Debug.Log("wave started!");

        SpawnEnemies();
    }

    void SpawnEnemies() {
        while (difficulty < nextWaveNeededDifficulty) {
            Debug.Log("spawning enemy");
            Enemy enemyToSpawn = spawnables[Random.Range(0, spawnables.Length)];
            GameObject newEnemy = Instantiate(enemyToSpawn.prefab, new Vector2(0, 0), Quaternion.identity);

            //random pos

            difficulty += enemyToSpawn.difficultyValue;
        }
        nextWaveNeededDifficulty *= difficultyMultiplier;

        newWaveStarted = false;
    }
}
