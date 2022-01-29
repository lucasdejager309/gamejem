using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : Singleton<Gamemanager> {
    EnemySpawner spawner;
    public GameObject player;
    
    void Awake() {
        Instance = this;
        spawner = GetComponent<EnemySpawner>();
    }

    void Start() {
        player = GameObject.FindGameObjectWithTag("PlayerShip");
    }

    void Update() {
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("PlayerShip");
        }
    }

    public void StartEnemyWaves() {
        spawner.doWaves = true;
    }

    public void StopEnemyWaves() {
        spawner.doWaves = false;
    }
}
