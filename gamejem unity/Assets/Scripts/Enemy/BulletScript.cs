using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float damage;
    public string[] ignoreTags;

    void OnTriggerEnter2D(Collider2D other) {
        bool doDamage = true;
        foreach(string tag in ignoreTags) {
            if (other.tag == tag) {
                doDamage = false;
            }
        }
        
        if (doDamage) {
            IDamagable dam = other.GetComponent<IDamagable>();
            if (dam != null) {
                dam.TakeDamage(damage);
            }

            DestroyBullet();
        }
    }

    public void DestroyBullet() {
        Destroy(gameObject);
    }
}
