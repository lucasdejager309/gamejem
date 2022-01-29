using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmGrabber : MonoBehaviour
{

    private Collider2D target;
    public bool Engaged = false;

    // Start is called before the first frame update
    void Start()
    {
        target = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        BoxCollider2D Coll = GetComponent<BoxCollider2D>();
        
        if(Input.GetKeyDown(KeyCode.RightShift) && target != Coll)
        {
            target.gameObject.transform.parent = transform;
            Engaged = true;
        }

        if(Input.GetKeyUp(KeyCode.RightShift) && Engaged && target != Coll)
        {
            //target.gameObject.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
            
            target.gameObject.transform.parent = null;
            
            Engaged = false;
        }




    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pickup"))
        {
            print("Hahanow");

            target = other;
        }
        return;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if(target == collision && !Engaged)
        {
            target = GetComponent<BoxCollider2D>();
        }
    }
}
