using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmTarget : MonoBehaviour
{

    public float TargetMoveSpeed = 0.2f;
    public float ArmMaxLength = 7;
    public float TargetPullbackSpeed = 0.1f;

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
        Transform target = GameObject.FindGameObjectWithTag("ArmLocation").transform;
        Vector2 toVec = new Vector2(target.position.x, target.position.y) - new Vector2(transform.position.x, transform.position.y);
        
        if (toVec.magnitude > TargetPullbackSpeed)
        {
            toVec = toVec.normalized * TargetPullbackSpeed;

            transform.position += new Vector3(toVec.x, toVec.y, 0);

        }


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
