using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    int currentSceneIndex;
    int totalSceneCount;
    void Start() // get scene index upon initialization of parent object (the rocket ship)
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        totalSceneCount = SceneManager.sceneCountInBuildSettings;
    }

    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Start":
                Debug.Log("This is the starting point. Get going!");
                break;
            case "Finish":
                Invoke("LoadNextLevel", 1.0f);
                break;
            default: // reset level on ship crash
                Invoke("ReloadLevel", 1.0f);
                break;
        }
    }

    void ReloadLevel()
    {
        Debug.Log("Ship crashed, respawning at launch pad.");
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        if (currentSceneIndex + 1 < totalSceneCount)
        {
            Debug.Log("Destination reached, moving to next level.");
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else
        {
            Debug.Log("Congratulations on finishing the game!");
            SceneManager.LoadScene(0);
        }
    }
}
