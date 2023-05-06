using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiCarReset : MonoBehaviour
{
    public float elapsedtime;
    Checkpoints checkpoints;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        checkpoints = GetComponent<Checkpoints>();
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.velocity.magnitude <= 1) 
        {
            elapsedtime = elapsedtime + Time.deltaTime;
        }
        else 
        {
            elapsedtime = 0;
        }
        if(elapsedtime > 10) 
        {
            gameObject.transform.position = checkpoints.PrevCheckpoint.transform.position;
            gameObject.transform.rotation = checkpoints.PrevCheckpoint.transform.rotation;
        }
    }
}
