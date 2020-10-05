using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicCloneController : MonoBehaviour
{
    public GameObject Original;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Rigidbody2D origRB = Original.GetComponent<Rigidbody2D>();

        //copy values from one to the other
        //except position, that needs to be done prior
        rb.rotation = origRB.rotation;
        rb.velocity = origRB.velocity;
        rb.angularVelocity = origRB.angularVelocity;
        tag = "Clone";

    }

    
    void FixedUpdate()
    {
        if (Original == null)
        {
            Destroy(gameObject);
            return;
        }
        //sync stats
        Rigidbody2D origRB = Original.GetComponent<Rigidbody2D>();

        //copy values from one to the other
        //except position, that needs to be done prior
        rb.rotation = origRB.rotation;
        rb.velocity = origRB.velocity;
        rb.angularVelocity = origRB.angularVelocity;

        //todo: sync positions
    }
}
