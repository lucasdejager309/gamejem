using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy {
    public GameObject prefab;
    public float difficultyValue;
}

public class EnemySpawner : MonoBehaviour
{
    public bool doWaves = false;
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

    bool newWaveStarted;
    Transform player;

    void Start() {
        player = GameObject.FindGameObjectWithTag("PlayerShip").transform;
    }

    void Update() {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemyCount = enemies.Length;

        if (enemyCount == 0 && !newWaveStarted && doWaves) {
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
        difficulty = 0;

        while (difficulty < nextWaveNeededDifficulty) {
            bool foundEnemyToSpawn = false;

            Enemy enemyToSpawn = spawnables[Random.Range(0, spawnables.Length)];
            GameObject newEnemy = Instantiate(enemyToSpawn.prefab, new Vector2(0, 0), Quaternion.identity);

            Vector2 pos = new Vector2();
            bool foundGoodPos = false;
            while (!foundGoodPos) {
                pos.x = Random.Range(player.position.x-maxSpawnDistanceFromPlayer, player.position.x+maxSpawnDistanceFromPlayer);
                pos.y = Random.Range(player.position.x-maxSpawnDistanceFromPlayer, player.position.x+maxSpawnDistanceFromPlayer);

                if (Vector2.Distance(player.transform.position, pos) > minSpawnDistanceFromPlayer) {
                    foundGoodPos = true;
                }
            }

            newEnemy.transform.position = new Vector2(pos.x, pos.y);

            difficulty += enemyToSpawn.difficultyValue;
        }
        nextWaveNeededDifficulty *= difficultyMultiplier;
        waveCount++;

        newWaveStarted = false;
    }
}
