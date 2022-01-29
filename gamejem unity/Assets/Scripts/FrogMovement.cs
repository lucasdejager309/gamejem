using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogMovement : MonoBehaviour
{
    public Vector2 ShipOffset;
    public float StartDegrees = 0;
    public float Angle;
    public float MoveSpeed = 5;


    // Start is called before the first frame update
    void Start()
    {
        ShipOffset = new Vector2(transform.position.x, transform.position.y);
        Angle = StartDegrees;
    }
    public static Vector2 Rotate(Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }


    // Update is called once per frame
    void Update()
    {
        Vector2 RelativePos = Rotate(ShipOffset, Angle);
        transform.position = new Vector3(RelativePos.x, RelativePos.y, -5) + GameObject.FindGameObjectWithTag("PlayerShip").transform.position;
        transform.rotation = Quaternion.Euler(0, 0, Angle);

    }

    private void FixedUpdate()
    {
        //Rotate Based on input
        if (Input.GetKey(KeyCode.L))
        {
            //Can I move here with the other component in the way?
            Angle += MoveSpeed;
        }
        if (Input.GetKey(KeyCode.J))
        {
            //Can I move here with the other component in the way?
            Angle -= MoveSpeed;
        }
        Angle = Angle % 360;
    }
}
