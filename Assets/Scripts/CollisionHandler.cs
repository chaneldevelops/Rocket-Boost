using UnityEngine;
using UnityEngine.SceneManagement;//we're using SceneManager from this namespace

public class CollisionHandler : MonoBehaviour
{
    [SerializeField]float levelDelay = 2f;
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
            //Add sound effect for crashing 
            //Add particle effect on crash
            GetComponent<Movement>().enabled = false;
            Invoke("LoadNextLevel", levelDelay);
        }

        void StartCrashSequence() //when a player crashes there's a delay and movement controls are disabled
        {
            //Add sound effect for crashing 
            //Add particle effect on crash
            GetComponent<Movement>().enabled = false;
            Invoke("ReloadLevel", levelDelay); //Delays the reload by one second
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
