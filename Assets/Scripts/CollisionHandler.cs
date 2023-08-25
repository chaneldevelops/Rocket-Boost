using UnityEngine;
using UnityEngine.SceneManagement;//we're using SceneManager from this namespace

public class CollisionHandler : MonoBehaviour
{

    // Parameters - for tuning, are usually set in the editor e.g. mainThrust, so things with SerializeField
    // Cache - e.g. references for readability or speed e.g. Rigidbody or Audiosource
    // State - private instance (member) variables

    [SerializeField]float levelDelay = 2f;
    [SerializeField] AudioClip rocketCrash;
    [SerializeField] AudioClip rocketSuccess;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


   void OnCollisionEnter(Collision other)
   {
        switch (other.gameObject.tag)
        {
            case "Friendly":
            Debug.Log("I'm friendly");
            break;

            case "Finish":
            StartSuccessSequence();
            break;

            default:
            StartCrashSequence();
            break;
        }

        void StartSuccessSequence() //when a player wins there's a delay and movement controls are disabled
        {
            GetComponent<Movement>().enabled = false;
            Invoke("LoadNextLevel", levelDelay);
            audioSource.PlayOneShot(rocketSuccess);//Add sound effect for crashing 
            
        }

        void StartCrashSequence() //when a player crashes there's a delay and movement controls are disabled
        {
            GetComponent<Movement>().enabled = false;
            Invoke("ReloadLevel", levelDelay); //Delays the reload by one second
            audioSource.PlayOneShot(rocketCrash);//Add particle effect on crash
        }
   }
   void ReloadLevel()
   {
      int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //Build Index is the scene index and Get Active Scene returns the active scene
      SceneManager.LoadScene(currentSceneIndex); //this reloads the scene when the rocket crashed
   } 

   void LoadNextLevel()
   {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    int nextSceneIndex = currentSceneIndex + 1; //whatever the current scene is plus an index scene
    if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
    {
        nextSceneIndex = 0; //when the levels/scenes are completed it goes back to level 1
    }
    SceneManager.LoadScene(nextSceneIndex);
   }
}
