using System;
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
    [SerializeField] ParticleSystem forwardThrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;

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
            PlayParticles("main");
            PlayThrusterAudio();
            rb.AddRelativeForce(Vector3.up * Time.deltaTime * forwardThrust);
        }
        else
        {
            forwardThrustParticles.Stop();
            audioSource.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            PlayParticles("left");
            ApplyRotation(rotationalThrust);
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            PlayParticles("right");
            ApplyRotation(-rotationalThrust);
        }

        else
        {
            rightThrustParticles.Stop();
            leftThrustParticles.Stop();
        }
    }

    void ApplyRotation(float rotationMagnitude)
    {
        // freeze physics system rotation to allow manual rotation
        // this resolves bug that removes player control upon collisions
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationMagnitude);
        rb.freezeRotation = false;
    }

    void PlayThrusterAudio()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrusterAudio);
        }
    }

    void PlayParticles(String thruster)
    {
        if (thruster == "main")
        {
            if (!forwardThrustParticles.isPlaying)
            {
                forwardThrustParticles.Play();
            }
        }

        if (thruster == "left")
        {
            if (!leftThrustParticles.isPlaying)
            {
                leftThrustParticles.Play();
            }
        }

        if (thruster == "right")
        {
            if (!rightThrustParticles.isPlaying)
            {
                rightThrustParticles.Play();
            }
        }
    }
}
