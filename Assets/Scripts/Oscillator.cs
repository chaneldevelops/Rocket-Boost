using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor; //adding range can make it into a slider
    [SerializeField] float period = 2f;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (period <= Mathf.Epsilon) {return;} //used Epsilon when comparing exact float values
        float cycles = Time.time / period;//measuring time, the complete lap around the pie circlev(x axis) continues to grow over time
        const float tau = Mathf.PI * 2; // tau is a constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); //give a value between 1 & -1
        
        movementFactor = rawSinWave;// controls the back and forth wave that goes from -1 to 1
        
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset; //defines the new position from the intial offset
    }
}
