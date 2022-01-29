using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public string targetTag;
    public float minOrbitDistance;
    public float maxOrbitDistance;
    public float attackRange;
    public float attackSpeed;
    public float rotationSpeed;
    public float movementSpeed;
    public float acceleration;
    public Transform bulletOrigin;
    public GameObject bulletPrefab;
    public float bulletSpread;
    public float bulletSpeed;
    GameObject target;
    
    float currentDistance;
    float sinceLastShot;

    Rigidbody2D rb;

    enum enemyState {
        charge,
        circle,
        flee
    }

    enemyState state;

    void Start() {
        target = GameObject.FindGameObjectWithTag(targetTag);
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        currentDistance = Vector2.Distance(transform.position, target.transform.position);

        if (target != null) {
            switch (state) {
                case enemyState.charge:
                    Charge();
                    if (currentDistance < maxOrbitDistance) {
                        rb.velocity = new Vector2(0,0);
                        state = enemyState.circle;
                    } else if (currentDistance < minOrbitDistance) {
                        state = enemyState.flee;
                    }
                    break;
                case enemyState.circle:
                    CircleTarget();
                    if (currentDistance > maxOrbitDistance+maxOrbitDistance/4) {
                        state = enemyState.charge;
                    } else if (currentDistance < minOrbitDistance) {
                        state = enemyState.flee;
                    }
                    break;

                case enemyState.flee:
                    Flee();
                    if (currentDistance > minOrbitDistance) {
                        state = enemyState.charge;
                    }
                    break;
            }
        }

        Move();

        if (currentDistance < attackRange) {
            Attack();
        }

        if (sinceLastShot > 0) {
            sinceLastShot -= Time.deltaTime;
        } else if (sinceLastShot < 0) {
            sinceLastShot = 0;
        }
    }

    void Charge() {
        float eulerRotZ = Quaternion.ToEulerAngles(transform.rotation).z;
 
        RotateTowards(target.transform.position);
        
    }

    void CircleTarget() {
        float angle = getRotZ(target.transform.position)+50;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, angle), Time.deltaTime*rotationSpeed);
    }   

    void RotateTowards(Vector2 point) {
        float rotZ = getRotZ(point);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, rotZ), Time.deltaTime*rotationSpeed);
    }

    float getRotZ(Vector2 point) {
        Vector3 difference = new Vector2(transform.position.x, transform.position.y) - point;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        return rotZ;
    }

    void Move() {
        rb.velocity -= new Vector2(transform.right.x, transform.right.y)*acceleration;
        if (rb.velocity.magnitude > movementSpeed) {
            rb.velocity = rb.velocity.normalized*movementSpeed;
        }
    }

    void Flee() {
        float angle = getRotZ(target.transform.position)+180;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0f, 0f, angle), Time.deltaTime*rotationSpeed);
    }

    void Attack() {
        if (sinceLastShot == 0) {
            Debug.Log("shoot");

            Vector3 difference = target.transform.position - bulletOrigin.position;
            float rotZ = (Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg) + Random.Range(-bulletSpread, bulletSpread);
            GameObject bullet = Instantiate(bulletPrefab, bulletOrigin.position, Quaternion.Euler(0f, 0f, rotZ));
            bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right*bulletSpeed;

            sinceLastShot = attackSpeed;
        }
    }
}
