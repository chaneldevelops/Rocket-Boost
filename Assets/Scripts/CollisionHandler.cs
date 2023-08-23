using UnityEngine;
using UnityEngine.SceneManagement;//we're using SceneManager from this namespace

public class CollisionHandler : MonoBehaviour
{
   void OnCollisionEnter(Collision other)
   {
        switch (other.gameObject.tag)
        {
            case "Friendly":
            Debug.Log("I'm friendly");
            break;

            case "Finish":
            LoadNextLevel();
            break;

            default:
            ReloadLevel();
            break;
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
