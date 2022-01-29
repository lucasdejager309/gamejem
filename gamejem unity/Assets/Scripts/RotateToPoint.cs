using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToPoint : MonoBehaviour
{
    public string tag;
    public GameObject point;
    public float rotationSpeed;

    void RotateTowards() {
     float rotZ = getRotZ(point);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, rotZ), Time.deltaTime*rotationSpeed);
    }

    float getRotZ(GameObject point) {
        Vector3 difference = new Vector2(transform.position.x, transform.position.y) - new Vector2(point.transform.position.x, point.transform.position.y);
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        return rotZ;
    }

    void Start() {
        point = GameObject.FindGameObjectWithTag(tag);
    }

    void Update() {
        RotateTowards();
    }
}
