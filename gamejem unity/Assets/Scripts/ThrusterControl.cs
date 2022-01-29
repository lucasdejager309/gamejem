using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class ThrusterControl : MonoBehaviour
{

    public Vector2 ShipOffset;
    public float StartDegrees = 0;
    public float Angle;
    public float RotateSpeed = 2;
    public float ThrustForce = 5;
    public float MaxSpeed = 150;

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
        transform.position = new Vector3(RelativePos.x, RelativePos.y, -2) + GameObject.FindGameObjectWithTag("PlayerShip").transform.position;
        transform.rotation = Quaternion.Euler(0,0,Angle);

    }

    private void FixedUpdate()
    {
        //Rotate Based on input
        if (Input.GetKey(KeyCode.A)) {
            //Can I move here with the other component in the way?
            Angle += RotateSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            //Can I move here with the other component in the way?
            Angle -= RotateSpeed;
        }
        Angle = Angle % 360;


        Rigidbody2D Rigid = GameObject.FindGameObjectWithTag("PlayerShip").GetComponent<Rigidbody2D>();

        //Thrust
        if (Input.GetKey(KeyCode.S))
        {
            Rigid.AddForce(Rotate(new Vector2(0,-1), Angle) * ThrustForce);
        }
        //Limit speed
        if (Rigid.velocity.magnitude > MaxSpeed)
        {
            Rigid.velocity = Rigid.velocity.normalized * MaxSpeed;
        }


    }
}
