using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public string targetTag;
    GameObject target;

    enum enemyState {
        charge,
        circle
    }

    enemyState state;

    void Start() {
        target = GameObject.FindGameObjectWithTag(targetTag);
    }

    void Update()
    {
        if (target != null) {
            switch (state) {
                case enemyState.charge:

                    break;
                case enemyState.circle:
                    break;
            }
        }
    }
}
