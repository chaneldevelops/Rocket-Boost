using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // Parameters - for tuning, are usually set in the editor e.g. mainThrust, so things with SerializeField
    // Cache - e.g. references for readability or speed e.g. Rigidbody or Audiosource
    // State - private instance (member) variables

    [SerializeField] float mainThrust = 100;
    [SerializeField] float mainRotate = 50;
    [SerializeField] AudioClip mainEngine; //main engine audio

    [SerializeField] ParticleSystem mainBoostersParticlesFrontLeft;
    [SerializeField] ParticleSystem mainBoostersParticlesFrontRight;
    [SerializeField] ParticleSystem mainBoostersParticlesBackLeft;
    [SerializeField] ParticleSystem mainBoostersParticlesBackRight;
    [SerializeField] ParticleSystem leftBoosterParticle;
    [SerializeField] ParticleSystem rightBoosterParticle;

    Rigidbody rb;// created a reference
    AudioSource audioSource;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); //this is caching a reference to our component
        audioSource = GetComponent<AudioSource>();
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
                rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime); //Add the force relative to it's transform information 
                    //The 1 1 1 represent the vector X, Y, & Z
                if (!audioSource.isPlaying)
                {
                    audioSource.PlayOneShot(mainEngine); //play is a method fo AudioSource, with play one shot can select the music in parameter 
                }
                MainBoosters();

            } 
            else
            {
                audioSource.Stop(); //Stop is a method of AudioSource
                //down here is where the stop for the boosters would go but instead
                //I disabled the loop option which worked for my game design
            }
        }

        void MainBoosters() //created a method to clean up code for the four boosters
        {
            mainBoostersParticlesFrontLeft.Play();
            mainBoostersParticlesFrontRight.Play();
            mainBoostersParticlesBackLeft.Play();
            mainBoostersParticlesBackRight.Play();

        }
    
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
            {
               ApplyRotation(mainRotate);
               leftBoosterParticle.Play();
            }
            else if (Input.GetKey(KeyCode.D)) //If we're not satisfying the condition above then execute
            {
               ApplyRotation(-mainRotate); //this is the negative forward
               rightBoosterParticle.Play();
            }

            void ApplyRotation(float rotationThisFrame)
            {
                rb.freezeRotation = true; //freezing rotation so can manually rotate
                transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime); //left is the default if both are being pressed
                rb.freezeRotation = false; //unfreezing rotation so physics system can take over
            }
    }
    
}
