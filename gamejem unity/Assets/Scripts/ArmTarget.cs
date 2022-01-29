using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmTarget : MonoBehaviour
{

    public float TargetMoveSpeed;
    public float ArmMaxLength;

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
        ;

        if(Input.GetKey(KeyCode.I))
        {
            transform.position += new Vector3(0, TargetMoveSpeed, 0);
        }
        if (Input.GetKey(KeyCode.K))
        {
            transform.position += new Vector3(0, -TargetMoveSpeed, 0);
        }
        if (Input.GetKey(KeyCode.L))
        {
            transform.position += new Vector3(TargetMoveSpeed, 0, 0);
        }
        if (Input.GetKey(KeyCode.J))
        {
            transform.position += new Vector3(-TargetMoveSpeed, 0, 0);
        }

        if(transform.localPosition.magnitude > ArmMaxLength)
        {

            transform.localPosition = transform.localPosition.normalized * ArmMaxLength;
        }

    }
}
