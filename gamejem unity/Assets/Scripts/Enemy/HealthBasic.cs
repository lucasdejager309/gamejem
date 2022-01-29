using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBasic : MonoBehaviour, IDamagable {
    [SerializeField] private float health;
    public float maxHealth = 8;
    public float Health {
        get { return health; }
    }

    public virtual void TakeDamage(float damage) {
        health -= damage; 
    }

    void Start() {
        health = maxHealth;
    }

    void Update() {
        if (health <= 0) {
            Die();
        }
    }

    public virtual void Die() {
        //death goes here
    }

    public void ResetHealth() {
        health = maxHealth;
    }
}