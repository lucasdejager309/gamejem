using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentRotator : MonoBehaviour
{
    public Vector2 ShipOffset;
    public float StartDegrees;
    public float Angle;
    public float RotateSpeed;

    public KeyCode Left;
    public KeyCode Right;

    public float BlockRange;

    // Start is called before the first frame update
    void Start()
    {
        ShipOffset = new Vector2(transform.localPosition.x, transform.localPosition.y);
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
        transform.position = new Vector3(RelativePos.x, RelativePos.y, -3) + GameObject.FindGameObjectWithTag("PlayerShip").transform.position;
        transform.rotation = Quaternion.Euler(0, 0, Angle);

    }

    private void FixedUpdate()
    {
        GameObject[] Obstructors = GameObject.FindGameObjectsWithTag("RotateObstructor");
        List<float> CantRotate = new List<float>();

        //Get all the other modules that can block this one
        foreach (GameObject Object in Obstructors)
        {
            if(Object != this.gameObject)
            {
                CantRotate.Add(Object.GetComponent<AttachmentRotator>().Angle);
            }
        }

        //Rotate Turret
        if (Input.GetKey(Left))
        {
            //Can I move here with the other component in the way?
            bool CanGo = true;

            foreach( float A in CantRotate)
            {
                if (Mathf.Abs(Angle + RotateSpeed - A) < BlockRange)
                {
                    CanGo = false;
                }
            }

            if(CanGo)
            {
                Angle += RotateSpeed;
            }
            
        }

        if (Input.GetKey(Right))
        {
            //Can I move here with the other component in the way?
            bool CanGo = true;

            foreach (float A in CantRotate)
            {
                if (Mathf.Abs(Angle - RotateSpeed - A) < BlockRange)
                {
                    CanGo = false;
                }
            }

            if (CanGo)
            {
                Angle -= RotateSpeed;
            }
        }
        Angle = (Angle + 360) % 360;
    }
}
