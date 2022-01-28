using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public string targetTag;
    public float attackDistance;
    public float rotationSpeed;
    public float chargeRotSpread;
    public float movementSpeed;
    public float acceleration;
    GameObject target;
    
    float currentDistance;

    Rigidbody2D rb;

    enum enemyState {
        charge,
        circle
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
                    if (currentDistance < attackDistance) {
                        state = enemyState.circle;
                    }
                    break;
                case enemyState.circle:
                    CircleTarget();
                    if (currentDistance > attackDistance) {
                        state = enemyState.charge;
                    }
                    break;
            }
        }
    }

    void Charge() {
        float eulerRotZ = Quaternion.ToEulerAngles(transform.rotation).z;
        

        rb.velocity -= new Vector2(transform.right.x, transform.right.y)*acceleration;
        if (rb.velocity.magnitude > movementSpeed) {
            rb.velocity = rb.velocity.normalized*movementSpeed;
        }

        

        
        RotateTowards(target.transform);
        
    }

    void CircleTarget() {

    }   

    void RotateTowards(Transform point) {
        float rotZ = getRotZ(point);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, rotZ), Time.deltaTime*rotationSpeed);
    }

    float getRotZ(Transform point) {
        Vector3 difference = transform.position - point.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        return rotZ;
    }
}
