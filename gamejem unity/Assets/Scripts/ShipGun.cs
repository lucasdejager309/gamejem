using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGun : MonoBehaviour
{
    public float RotationSpeed;
    public float ShootCooldown;
    private float CurrentCooldown = 0;
    public float Bulletspeed;
    public float RotationRange;
    private float MinRotationRange;
    public int ammo;
    public int maxAmmo;

    bool reloading;

    public GameObject BulletPrefab;

    private void Start()
    {
        MinRotationRange = 360 - RotationRange;
        ammo = maxAmmo;
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.J))
        { 
            //transform.position += new Vector3(TargetMoveSpeed, 0, 0);
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + RotationSpeed);
        }
        if (Input.GetKey(KeyCode.L))  //&& Mathf.Abs(( transform.localRotation.eulerAngles.z - 360) - RotationSpeed) < RotationRange)
        {
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - RotationSpeed);
                
        }

        if(transform.localRotation.eulerAngles.z > RotationRange && transform.localRotation.eulerAngles.z < 180)
        {
            transform.localRotation = Quaternion.Euler(0, 0, RotationRange);
        }
        else if(transform.localRotation.eulerAngles.z < MinRotationRange && transform.localRotation.eulerAngles.z > 180)
        {
            transform.localRotation = Quaternion.Euler(0, 0, MinRotationRange);
        }

        print(transform.localRotation.eulerAngles.z);
        if (ShootCooldown < CurrentCooldown && ammo != 0) {
            if (Input.GetKey(KeyCode.RightShift))
            {
                Shoot();
                ammo--;
                Debug.Log("ammo left: " + ammo);
            }
        }
        else
        {
            CurrentCooldown += Time.deltaTime;
        }

    }

    void Shoot() {
        GameObject Bullet = Instantiate(BulletPrefab, transform.position, transform.rotation);
        Bullet.GetComponent<Rigidbody2D>().velocity = Bullet.GetComponent<Transform>().up * Bulletspeed;
        CurrentCooldown = 0;
    }

    public void Reload(int ammoAmount) {
        ammo += ammoAmount;
        if (ammo > maxAmmo) {
            ammo = maxAmmo;
        }
    }

}
