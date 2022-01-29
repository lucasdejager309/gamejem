using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HealthBasic
{
    public float dealDamageOnCrash;
    public string[] crashIgnoreTags;
    DropLoot dropLoot;

    public override void Die()
    {
        
        dropLoot = GetComponent<DropLoot>();
        dropLoot.Drop();
        GameObject.Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other) {
        
        bool doDamage = true;
        foreach(string tag in crashIgnoreTags) {
            if (other.tag == tag) {
                doDamage = false;
            }
        }
        
        if (doDamage) {
            Debug.Log(other.name);

            IDamagable dam = other.GetComponent<IDamagable>();
            if (dam != null) {
                dam.TakeDamage(dealDamageOnCrash);
            }
            TakeDamage(Health);
        }
    }
}
