using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGun : MonoBehaviour
{
    public float RotationSpeed;
    public float ShootCooldown;
    public float Bulletspeed;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.J))
        {
            //transform.position += new Vector3(TargetMoveSpeed, 0, 0);
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + RotationSpeed);
        }
        if (Input.GetKey(KeyCode.L))
        {
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - RotationSpeed);
        }


    }

}
