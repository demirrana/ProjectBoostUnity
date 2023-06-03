using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The class that lets the obstacle move between two points continuously
public class Oscillator : MonoBehaviour
{
    Vector3 initialPos;

    [SerializeField] Vector3 movementVector;

    float movementFactor;
    float period = 2f;

    void Start()
    {
        initialPos = transform.position;
    }

    void Update()
    {
        if (period <= Mathf.Epsilon)
            return;
        float tau = 2 * Mathf.PI;
        float cycle = Time.time / period; //The times that the movement is finished for all the time passed till now
        movementFactor = (Mathf.Sin(tau*cycle) + 1f) / 2;
        transform.position = initialPos + movementVector*movementFactor;
    }
}
