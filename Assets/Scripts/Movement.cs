using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // cache a reference to the Rigidbody class
    Rigidbody rb;
    [SerializeField] float forwardThrust = 800.0f;
    [SerializeField] float rotationalThrust = 20.0f;

    // Start is called before the first frame update
    void Start()
    {
        //instantiate a Rigidbody object at game start
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * Time.deltaTime * forwardThrust);
        }
    }
    
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            ApplyRotation(rotationalThrust);
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            ApplyRotation(-rotationalThrust);
        }
    }

    private void ApplyRotation(float rotationMagnitude)
    {
        // freeze physics system rotation to allow manual rotation
        // this resolves bug that removes player control upon collisions
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationMagnitude);
        rb.freezeRotation = false;
    }
}
