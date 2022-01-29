using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthBasic
{
    public float dealDamageOnCrash;
    public string[] crashIgnoreTags;
    
    public override void Die()
    {
        GameObject.Destroy(gameObject);
        //fiery explosion and stuff
    }

    void OnTriggerEnter2D(Collider2D other) {
        bool doDamage = true;
        foreach(string tag in crashIgnoreTags) {
            if (other.tag == tag) {
                doDamage = false;
            }
        }
        
        if (doDamage) {
            IDamagable dam = other.GetComponent<IDamagable>();
            if (dam != null) {
                dam.TakeDamage(dealDamageOnCrash);
            }
            Die();
        }
    }
}
