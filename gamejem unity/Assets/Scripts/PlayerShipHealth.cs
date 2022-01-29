using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipHealth : HealthBasic
{
    [SerializeField] private float shield;
    public float maxShield = 8;
    public float Shield {
        get { return shield; }
    }
    public bool doRegen;
    public float regenRate;
    float sinceLastRegen;

    void Start() {
        shield = maxShield;
    }

    void Update() {
        if (doRegen) {
            Regen();

            if (sinceLastRegen > 0) {
            sinceLastRegen -= Time.deltaTime;
            } else if (sinceLastRegen < 0) {
                sinceLastRegen = 0;
            }
        }
    }

    public override void TakeDamage(float damage)
    {
        
        float damageToTake = damage;

        if (shield > damage) {
            shield -= damageToTake;
        } else {
            damageToTake -= shield;
            shield = 0;
            base.TakeDamage(damageToTake);
        }
    }

    public override void Die()
    {
        Debug.Log("ur dead");
        //RESTART GAME OR SOMETHIN;
    }

    void Regen() {
        if (sinceLastRegen == 0 && shield < maxShield) {
            shield++;
            if (shield > maxShield) shield = maxShield;
            sinceLastRegen = regenRate;
        }
    }

}
