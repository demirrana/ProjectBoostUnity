using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;
    [SerializeField] AudioClip thrustSound;

    [Tooltip("The thrust force that is applied on the rocket.")]
    [SerializeField] float mainThrust = 500f;
    [Tooltip("The rotational force that is applied on the rocket.")]
    [SerializeField] float rotationRate = 50f; 

    [Tooltip("The particle system that is used as the thrusting particles when space is held.")]
    [SerializeField] ParticleSystem thrustParticles;
    [Tooltip("The particle system that is used as the thrusting particles when the rocket rotates towards right.")]
    [SerializeField] ParticleSystem leftThrustParticles;
    [Tooltip("The particle system that is used as the thrusting particles when the rocket rotates towards left.")]
    [SerializeField] ParticleSystem rightThrustParticles;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        thrustParticles.Stop();
    }

    void StartThrusting()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
        if (!thrustParticles.isPlaying)
        {
            thrustParticles.Play();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateToLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateToRight();
        }
        else
        {
            StopRotating();
        }
    }

    void RotateToLeft()
    {
        RotationMethod(rotationRate);
        leftThrustParticles.Play();
    }

    void RotateToRight()
    {
        RotationMethod(-rotationRate);
        rightThrustParticles.Play();
    }

    void StopRotating()
    {
        leftThrustParticles.Stop();
        rightThrustParticles.Stop();
    }

    void RotationMethod(float rotationRate)
    {
        rigidBody.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationRate * Time.deltaTime);
        rigidBody.freezeRotation = false;
    }

}
