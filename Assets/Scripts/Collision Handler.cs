using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    int currentSceneIndex;
    void Start() // get scene index upon initialization of parent object (the rocket ship)
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Start":
                Debug.Log("This is the starting point. Get going!");
                break;
            case "Finish":
                Debug.Log("Ship has reached destination. Stage complete.");
                break;
            default: // reset level on ship crash
                ReloadLevel();
                break;
        }
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }
}
