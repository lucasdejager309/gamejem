using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmGrabber : MonoBehaviour
{

    private Collider2D target;
    public bool Engaged = false;
    public Sprite Open;
    public Sprite Closed;

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
        
        if(Input.GetKeyDown(KeyCode.RightShift))
        {
            if (target != Coll) {
                target.gameObject.transform.parent = transform;
                target.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            }
            Engaged = true;
            GetComponent<SpriteRenderer>().sprite = Closed;

        }

        if(Input.GetKeyUp(KeyCode.RightShift))
        {
            //target.gameObject.GetComponent<Rigidbody2D>().velocity = GetComponent<Rigidbody2D>().velocity;
            if (Engaged && target != Coll) {
                target.gameObject.transform.parent = null;
            }
            Engaged = false;
            GetComponent<SpriteRenderer>().sprite = Open;
        }





    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Pickup"))
        {
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
