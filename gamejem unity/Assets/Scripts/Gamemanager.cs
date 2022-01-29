using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gamemanager : Singleton<Gamemanager> {
    EnemySpawner spawner;
    
    void Awake() {
        Instance = this;
        spawner = GetComponent<EnemySpawner>();
    }


    public void StartEnemyWaves() {
        spawner.doWaves = true;
    }

    public void StopEnemyWaves() {
        spawner.doWaves = false;
    }
}
