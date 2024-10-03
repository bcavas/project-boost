using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // cache references to reused objects
    Rigidbody rb;
    AudioSource audioSource;

    // parameters for editor input
    [SerializeField] float forwardThrust = 800.0f;
    [SerializeField] float rotationalThrust = 20.0f;
    [SerializeField] AudioClip thrusterAudio;

    // Start is called before the first frame update
    void Start()
    {
        //instantiate reusable objects at game start
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        // log error messages for missing components
        if (rb == null)
        {
            Debug.LogError("Rigidbody component is missing from this GameObject");
        }
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing from this GameObject.");
        }
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
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(thrusterAudio);
            }
        }
        else
        {
            audioSource.Stop();
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
