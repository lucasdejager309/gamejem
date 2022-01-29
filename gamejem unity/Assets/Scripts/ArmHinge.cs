using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmHinge : MonoBehaviour
{

    public float RotateSpeed = 1;
    public float MinTargetAccuracy = 2;

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
        //Stupid, but check rotation both ways and pick the one that gets the best distance;

        GameObject ArmLocation = GameObject.FindGameObjectWithTag("ArmLocation");
        GameObject ArmTarget =  GameObject.FindGameObjectWithTag("ArmTarget");

        

        float ArmDistance = Vector2.Distance(
            new Vector2(ArmLocation.transform.position.x, ArmLocation.transform.position.y),
            new Vector2(ArmTarget.transform.position.x, ArmTarget.transform.position.y));

        if(ArmDistance < MinTargetAccuracy)
        {
            print("NotEvenClose");
            return;
        }

        Transform LocalTransform = transform;

        //transform.localRotation.SetEulerAngles(transform.rotation.eulerAngles + new Vector3(0, 0, -RotateSpeed));
        transform.localRotation = Quaternion.Euler(0, 0, transform.localRotation.eulerAngles.z - RotateSpeed * Mathf.Deg2Rad);

        float MinusDistance = Vector2.Distance(
            new Vector2(ArmLocation.transform.position.x, ArmLocation.transform.position.y),
            new Vector2(ArmTarget.transform.position.x, ArmTarget.transform.position.y));

        transform.rotation = LocalTransform.rotation;

        //transform.localRotation.SetEulerAngles(transform.rotation.eulerAngles + new Vector3(0, 0, RotateSpeed));
        transform.localRotation = Quaternion.Euler(0, 0, transform.localRotation.eulerAngles.z + RotateSpeed * Mathf.Deg2Rad);

        float PosDistance = Vector2.Distance(
            new Vector2(ArmLocation.transform.position.x, ArmLocation.transform.position.y),
            new Vector2(ArmTarget.transform.position.x, ArmTarget.transform.position.y));

        transform.rotation = LocalTransform.rotation;


        if (ArmDistance < MinusDistance && ArmDistance < PosDistance)
        {
            print("No");
            return;
        }
        else if(MinusDistance < PosDistance)
        {

            
            print("Min");
            //transform.localRotation.SetEulerAngles(transform.rotation.eulerAngles + new Vector3(0, 0, -RotateSpeed));
            transform.localRotation = Quaternion.Euler(0,0,  transform.localRotation.eulerAngles.z - RotateSpeed * Mathf.Deg2Rad);


        }
        else
        {
            print("Max");
            //transform.localRotation.SetEulerAngles(transform.rotation.eulerAngles + new Vector3(0, 0, RotateSpeed));
            transform.localRotation = Quaternion.Euler(0, 0, transform.localRotation.eulerAngles.z + RotateSpeed * Mathf.Deg2Rad);
        }

        return;
    }
}
